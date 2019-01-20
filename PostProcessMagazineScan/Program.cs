using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;

namespace PostProcessMagazineScan
{
    class Program
    {
        public const string Version = "1.1";

        private static void Error(string error)
        {
            Console.Error.WriteLine("Error: " + error);
            Environment.Exit(1);
        }

        private class ProcessSettings
        {
            public string InputDir = null;
            public string OutputDir = null;
            public string InputExt = "jpg";
            public string OutputExt = "png";
            public RotateFlipType Rotate = RotateFlipType.RotateNoneFlipNone;
            public uint CropLeft = 0;
            public uint CropRight = 0;
            public uint CropTop = 0;
            public uint CropBottom = 0;
            public string OutputPrefix = "IMG";
            public string OutputNumFormat = "000";
            public uint OutputBasenum = 1;
            public bool DoublePaging = false;
            public bool PagingRightToLeft = false;
            public bool Reorder = true;
            public bool Backflip = false;
        }

        private static ProcessSettings ParseArgs(string[] args)
        {
            ProcessSettings settings = new ProcessSettings();
            char[] invalidFnameChars = Path.GetInvalidFileNameChars();

            Queue<string> remainingArgs = new Queue<string>(args);

            while (remainingArgs.Count > 0)
            {
                string argName = remainingArgs.Dequeue();
                if (argName.StartsWith("--"))
                {
                    argName = argName.Substring(2).ToLower();
                    if (remainingArgs.Count > 0)
                    {
                        string argValue = remainingArgs.Dequeue();
                        switch (argName)
                        {
                            case "input-dir":
                                settings.InputDir = argValue;
                                break;
                            case "input-ext":
                                settings.InputExt = argValue.ToLower();
                                break;
                            case "output-dir":
                                settings.OutputDir = argValue;
                                break;
                            case "output-ext":
                                settings.OutputExt = argValue.ToLower();
                                break;
                            case "rotate":
                                switch (argValue)
                                {
                                    case "0":
                                        settings.Rotate = RotateFlipType.RotateNoneFlipNone;
                                        break;
                                    case "90":
                                        settings.Rotate = RotateFlipType.Rotate90FlipNone;
                                        break;
                                    case "180":
                                        settings.Rotate = RotateFlipType.Rotate180FlipNone;
                                        break;
                                    case "270":
                                        settings.Rotate = RotateFlipType.Rotate270FlipNone;
                                        break;
                                    default:
                                        Error("Unsupported value for --rotate, expecting 0, 90, 180 or 270.");
                                        break;
                                }
                                break;
                            case "crop-left":
                                if (!uint.TryParse(argValue, out settings.CropLeft))
                                    Error("Invalid value for --crop-left");
                                break;
                            case "crop-right":
                                if (!uint.TryParse(argValue, out settings.CropRight))
                                    Error("Invalid value for --crop-right");
                                break;
                            case "crop-top":
                                if (!uint.TryParse(argValue, out settings.CropTop))
                                    Error("Invalid value for --crop-top");
                                break;
                            case "crop-bottom":
                                if (!uint.TryParse(argValue, out settings.CropBottom))
                                    Error("Invalid value for --crop-bottom");
                                break;
                            case "output-basename":
                                foreach (char c in argValue)
                                    if (invalidFnameChars.Contains(c))
                                        Error("Invalid file name prefix in --output-basename");
                                char[] baseNumChars = argValue.Reverse().TakeWhile(c => c >= '0' && c <= '9').Reverse().ToArray();
                                settings.OutputPrefix = argValue.Substring(0, argValue.Length - baseNumChars.Length);
                                if (baseNumChars.Length > 0)
                                {
                                    settings.OutputBasenum = uint.Parse(new String(baseNumChars));
                                    for (int i = 0; i < baseNumChars.Length; i++)
                                        baseNumChars[i] = '0';
                                    settings.OutputNumFormat = new String(baseNumChars);
                                }
                                break;
                            case "input-paging":
                                switch (argValue.ToLower())
                                {
                                    case "single":
                                        settings.DoublePaging = false;
                                        break;
                                    case "double":
                                        settings.DoublePaging = true;
                                        settings.PagingRightToLeft = false;
                                        break;
                                    case "double-rtl":
                                        settings.DoublePaging = true;
                                        settings.PagingRightToLeft = true;
                                        break;
                                    default:
                                        Error("Unknown paging mode for --input-paging, expecting 'single', 'double' or 'double-rtl'");
                                        break;
                                }
                                break;
                            case "reorder":
                                switch (argValue.ToLower())
                                {
                                    case "0":
                                    case "false":
                                        settings.Reorder = false;
                                        break;
                                    case "1":
                                    case "true":
                                        settings.Reorder = true;
                                        break;
                                    default:
                                        Error("Unknown boolean value for --reorder, expecting true/false");
                                        break;
                                }
                                break;
                            case "backflip":
                                switch (argValue.ToLower())
                                {
                                    case "0":
                                    case "false":
                                        settings.Backflip = false;
                                        break;
                                    case "1":
                                    case "true":
                                        settings.Backflip = true;
                                        break;
                                    default:
                                        Error("Unknown boolean value for --backflip, expecting true/false");
                                        break;
                                }
                                break;
                            default:
                                Error("Unknown argument: --" + argName);
                                break;
                        }
                    }
                    else
                    {
                        Error("Missing value for argument: --" + argName);
                    }
                }
                else
                {
                    Error("Unknown positional argument: " + argName);
                }
            }

            if (String.IsNullOrEmpty(settings.InputDir))
                Error("Missing argument: --input-dir. see --help for usage.");

            if (String.IsNullOrEmpty(settings.OutputDir))
                Error("Missing argument: --output-dir. see --help for usage.");

            return settings;
        }

        static void Main(string[] args)
        {
            if (args.Length > 0 && !args.Contains("--help"))
            {
                ProcessSettings settings = ParseArgs(args);

                Console.WriteLine("Reading input directory");

                if (!Directory.Exists(settings.InputDir))
                    Error("Input directory not found: " + settings.InputDir);

                string[] inputImages = Directory.EnumerateFiles(settings.InputDir, "*." + settings.InputExt).OrderBy(img => img).ToArray();
                List<string> outputImages = new List<string>();

                if (inputImages.Length == 0)
                    Error("No ." + settings.InputExt + " files in input directory!");

                Console.WriteLine("Input dir contains " + inputImages.Length + " images");

                if (inputImages.Length % 2 != 0 && settings.Reorder)
                    Error("Input image count is not even, cannot reorder.");

                if (!Directory.Exists(settings.OutputDir))
                    Directory.CreateDirectory(settings.OutputDir);

                for (int i = 0; i < inputImages.Length; i++)
                {
                    bool upsideDownImg = settings.Reorder && settings.Backflip && i >= inputImages.Length / 2;
                    string imgFile = inputImages[i];
                    Console.WriteLine("Processing: " + Path.GetFileName(imgFile));
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
                            Error("Crop values are higher than image size");
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

                Console.WriteLine((settings.Reorder ? "Reordering" : "Renaming") + " output files");

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
                    Console.WriteLine(Path.GetFileName(src) + " -> " + Path.GetFileName(dst));
                    File.Move(src, dst);
                }

                Console.WriteLine("Done.");
            }
            else
            {
                if (args.Contains("--help"))
                {
                    foreach (string line in new[]{
                        "== Paper Stack Scan Post-Processor v" + Program.Version + " - by ORelio - microzoom.fr ==",
                        " Post-process batches of automated scans: paper stacks and stack of unstapled magazine pages",
                        " This program can also batch-apply simple image operations such as rotate, crop and split",
                        "",
                        "== Usage ==",
                        " 1. Scan paper stack front pages, then reverse stack and scan back pages",
                        " 2. Launch PostProcess.exe --input-dir <dir> --output-dir <dir>",
                        " 3. Pages are automatically reordered with optional rotate, crop, split",
                        "",
                        "== Argument Name    Default        Description ==",
                        " --input-dir        (mandatory)    Input directory to read images from",
                        " --output-dir       (mandatory)    Output directory to write images to",
                        " --rotate           0              Rotate input images by 0, 90, 180 or 270 degrees",
                        " --crop-left        0              After rotating, remove pixel lines on the left",
                        " --crop-right       0              After rotating, remove pixel lines on the right",
                        " --crop-top         0              After rotating, remove pixel lines on the top",
                        " --crop-bottom      0              After rotating, remove pixel lines on the bottom",
                        " --input-ext        jpg            Input image file type, supports jpg, jpeg, png, bmp",
                        " --input-paging     single         'double'/'double-rtl': Split pages in 2 after rotate/crop",
                        " --output-ext       png            Output image file type, supports jpg, jpeg, png, bmp",
                        " --output-basename  IMG001         Base naming of output files, number must be placed last",
                        " --reorder          true           Reorder pages, assuming input is all front then all back",
                        " --backflip         false          Handle second half of the input stack placed upside down",
                        ""
                    })
                    {
                        Console.Error.WriteLine(line);
                    }
                }
                else
                {
                    Console.Error.WriteLine("Usage: PostProcess.exe --input-dir <dir> --output-dir <dir>");
                    Console.Error.WriteLine("See PostProcess.exe --help for detailed usage");
                }
            }
        }
    }
}
