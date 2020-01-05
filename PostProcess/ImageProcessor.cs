using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SharpTools;

namespace PostProcess
{
    /// <summary>
    /// Class holding the image processing procedure
    /// </summary>
    static class ImageProcessor
    {
        /// <summary>
        /// Run image processing using the provided settings
        /// </summary>
        /// <param name="settings">Settings for image processing</param>
        /// <param name="callbackOutput">Callback for output messages</param>
        /// <param name="callbackError">Callback for error messages</param>
        /// <returns>TRUE if successfully processed, FALSE otherwise</returns>
        public static bool Run(Settings settings, Action<string> callbackOutput, Action<string> callbackError)
        {
            callbackOutput(Translations.Get("process_status_reading_input_dir"));

            if (!Directory.Exists(settings.InputDir))
            {
                callbackError(String.Format(Translations.Get("process_error_indir_not_found"), settings.InputDir));
                return false;
            }

            string[] inputImages = Directory.EnumerateFiles(settings.InputDir, "*." + settings.InputExt).OrderBy(img => img).ToArray();
            List<string> outputImages = new List<string>();

            if (inputImages.Length == 0)
            {
                callbackError(String.Format(Translations.Get("process_error_no_input_files"), settings.InputExt));
                return false;
            }

            callbackOutput(String.Format(Translations.Get("process_status_input_dir_count"), inputImages.Length));

            if (inputImages.Length % 2 != 0 && settings.Reorder)
            {
                callbackError(Translations.Get("process_error_uneven_input_files"));
                return false;
            }

            if (!Directory.Exists(settings.OutputDir))
                Directory.CreateDirectory(settings.OutputDir);

            for (int i = 0; i < inputImages.Length; i++)
            {
                bool upsideDownImg = settings.Reorder && settings.Backflip && i >= inputImages.Length / 2;
                string imgFile = inputImages[i];
                callbackOutput(String.Format(Translations.Get("process_status_processing_file"), Path.GetFileName(imgFile), i + 1, inputImages.Length));
                Image loadImage = Image.FromFile(imgFile);
                Bitmap image = new Bitmap(loadImage);
                loadImage.Dispose();

                if (upsideDownImg)
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);

                if (settings.Rotate != RotateFlipType.RotateNoneFlipNone)
                    image.RotateFlip(settings.Rotate);

                if (settings.CropLeft > 0 || settings.CropRight > 0 || settings.CropTop > 0 || settings.CropBottom > 0)
                {
                    if (image.Width > settings.CropLeft + settings.CropRight
                        && image.Height > settings.CropTop + settings.CropBottom)
                    {
                        Bitmap image2 = image.Clone(
                            new Rectangle(
                                new Point(
                                    upsideDownImg ? (int)settings.CropRight : (int)settings.CropLeft,
                                    (int)settings.CropTop
                                ),
                                new Size(
                                    image.Width - (int)settings.CropLeft - (int)settings.CropRight,
                                    image.Height - (int)settings.CropTop - (int)settings.CropBottom
                                )
                            ),
                            image.PixelFormat
                        );
                        image.Dispose();
                        image = image2;
                    }
                    else
                    {
                        callbackError(Translations.Get("process_error_invalid_crop_values"));
                        return false;
                    }
                }

                if (settings.DoublePaging)
                {
                    Bitmap imageLeft = image.Clone(
                        new Rectangle(
                            Point.Empty,
                            new Size(image.Width / 2, image.Height)
                        ),
                        image.PixelFormat
                    );

                    Bitmap imageRight = image.Clone(
                        new Rectangle(
                            new Point(image.Width / 2, 0),
                            new Size(image.Width / 2, image.Height)
                        ),
                        image.PixelFormat
                    );

                    string outputLeft = String.Concat(
                        settings.OutputDir,
                        Path.DirectorySeparatorChar,
                        Path.GetFileNameWithoutExtension(imgFile),
                        "_1.",
                        settings.OutputExt
                    );

                    string outputRight = String.Concat(
                        settings.OutputDir,
                        Path.DirectorySeparatorChar,
                        Path.GetFileNameWithoutExtension(imgFile),
                        "_2.",
                        settings.OutputExt
                    );

                    imageLeft.Save(outputLeft);
                    imageRight.Save(outputRight);

                    outputImages.Add(outputLeft);
                    outputImages.Add(outputRight);

                    imageLeft.Dispose();
                    imageRight.Dispose();
                }
                else
                {
                    string outputSingle = String.Concat(
                        settings.OutputDir,
                        Path.DirectorySeparatorChar,
                        Path.GetFileNameWithoutExtension(imgFile),
                        '.',
                        settings.OutputExt
                    );

                    image.Save(outputSingle);
                    outputImages.Add(outputSingle);
                }

                image.Dispose();
            }

            callbackOutput(Translations.Get(settings.Reorder ? "process_status_reordering_files" : "process_status_renaming_files"));

            string[] reorderedImages = outputImages.ToArray();

            if (settings.Reorder)
            {
                int arrayMiddle = outputImages.Count / 2;
                if (settings.DoublePaging)
                {
                    for (int i = 0; i < arrayMiddle; i += 2)
                    {
                        if (settings.PagingRightToLeft)
                        {
                            string tmp = reorderedImages[i + 1];
                            reorderedImages[i + 1] = reorderedImages[reorderedImages.Length - i - 1];
                            reorderedImages[reorderedImages.Length - i - 1] = tmp;
                        }
                        else
                        {
                            string tmp = reorderedImages[i];
                            reorderedImages[i] = reorderedImages[reorderedImages.Length - i - 2];
                            reorderedImages[reorderedImages.Length - i - 2] = tmp;

                            tmp = reorderedImages[reorderedImages.Length - i - 2];
                            reorderedImages[reorderedImages.Length - i - 2] = reorderedImages[reorderedImages.Length - i - 1];
                            reorderedImages[reorderedImages.Length - i - 1] = tmp;

                            tmp = reorderedImages[i];
                            reorderedImages[i] = reorderedImages[i + 1];
                            reorderedImages[i + 1] = tmp;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < arrayMiddle; i++)
                    {
                        reorderedImages[i * 2] = outputImages[i];
                        reorderedImages[i * 2 + 1] = outputImages[outputImages.Count - i - 1];
                    }
                }
            }

            for (int i = 0; i < reorderedImages.Length; i++)
            {
                string src = reorderedImages[i];
                string dst = String.Concat(
                    settings.OutputDir,
                    Path.DirectorySeparatorChar,
                    settings.OutputPrefix,
                    (i + settings.OutputBasenum).ToString(settings.OutputNumFormat),
                    '.',
                    settings.OutputExt
                );
                callbackOutput(Path.GetFileName(src) + " -> " + Path.GetFileName(dst));
                File.Move(src, dst);
            }

            callbackOutput(Translations.Get("process_status_done"));
            return true;
        }
    }
}
