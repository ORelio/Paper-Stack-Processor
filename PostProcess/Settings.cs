using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using SharpTools;

namespace PostProcess
{
    /// <summary>
    /// Holds image processing settings
    /// </summary>
    public class Settings
    {
        public string InputDir = "input";
        public string OutputDir = "output";
        public string InputExt = "jpg";
        public string OutputExt = "png";
        public RotateFlipType Rotate = RotateFlipType.RotateNoneFlipNone;
        public uint CropLeft = 0;
        public uint CropRight = 0;
        public uint CropTop = 0;
        public uint CropBottom = 0;
        public string OutputPrefix = "IMG";
        public string OutputNumFormat = "0000";
        public uint OutputBasenum = 1;
        public bool DoublePaging = false;
        public bool PagingRightToLeft = false;
        public bool Reorder = true;
        public bool Backflip = false;

        /// <summary>
        /// Load settings from command-line arguments
        /// </summary>
        /// <param name="args">Command-line arguments</param>
        /// <exception cref="System.ArgumentException">Thrown on invalid argument</exception>
        /// <param name="allowMissingArgs">Allow missing mandatory arguments</param>
        /// <param name="settings">Initial set of settings to override</param>
        /// <returns>ProcessSettings object holding command-line settings</returns>
        public static Settings FromArgs(string[] args, bool allowMissingArgs = false, Settings settings = null)
        {
            if (settings == null)
                settings = new Settings();
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
                                        throw new ArgumentException(Translations.Get("argument_error_rotate"));
                                }
                                break;
                            case "crop-left":
                                if (!uint.TryParse(argValue, out settings.CropLeft))
                                    throw new ArgumentException(Translations.Get("argument_error_crop_left"));
                                break;
                            case "crop-right":
                                if (!uint.TryParse(argValue, out settings.CropRight))
                                    throw new ArgumentException(Translations.Get("argument_error_crop_right"));
                                break;
                            case "crop-top":
                                if (!uint.TryParse(argValue, out settings.CropTop))
                                    throw new ArgumentException(Translations.Get("argument_error_crop_top"));
                                break;
                            case "crop-bottom":
                                if (!uint.TryParse(argValue, out settings.CropBottom))
                                    throw new ArgumentException(Translations.Get("argument_error_crop_bottom"));
                                break;
                            case "output-basename":
                                try
                                {
                                    settings.OutputNaming = argValue;
                                }
                                catch (ArgumentException)
                                {
                                    throw new ArgumentException(Translations.Get("argument_error_outname"));
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
                                        throw new ArgumentException(Translations.Get("argument_error_paging"));
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
                                        throw new ArgumentException(Translations.Get("argument_error_reorder"));
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
                                        throw new ArgumentException(Translations.Get("argument_error_backflip"));
                                }
                                break;
                            default:
                                throw new ArgumentException(String.Format(Translations.Get("argument_error_unknown_arg"), "--" + argName));
                        }
                    }
                    else
                    {
                        throw new ArgumentException(String.Format(Translations.Get("argument_error_missing_value"), "--" + argName));
                    }
                }
                else
                {
                    throw new ArgumentException(String.Format(Translations.Get("argument_error_unknown_positional"), argName));
                }
            }

            if (!allowMissingArgs)
            {
                if (String.IsNullOrEmpty(settings.InputDir))
                    throw new ArgumentException(Translations.Get("argument_error_indir"));

                if (String.IsNullOrEmpty(settings.OutputDir))
                    throw new ArgumentException(Translations.Get("argument_error_outdir"));
            }

            return settings;
        }

        /// <summary>
        /// Load settings from a batch file.
        /// No exception is thrown on missing file or invalid settings.
        /// </summary>
        /// <param name="fileName">Batch file to load</param>
        /// <param name="settings">Initial set of settings to override</param>
        public static Settings FromBatchFile(string fileName, Settings settings = null)
        {
            if (settings == null)
                settings = new Settings();
            if (File.Exists(fileName))
            {
                foreach (string line in File.ReadAllLines(fileName))
                {
                    if (line.StartsWith("set "))
                    {
                        string[] nameValue = line.Substring(4).Split('=');
                        string settingName = nameValue[0].Trim();
                        string settingValue = nameValue[1].Trim();
                        string argName = null;
                        switch (settingName.ToLower())
                        {
                            case "indir": argName = "--input-dir"; break;
                            case "inpaging": argName = "--input-paging"; break;
                            case "inext": argName = "--input-ext"; break;
                            case "outdir": argName = "--output-dir"; break;
                            case "reorder": argName = "--reorder"; break;
                            case "backflip": argName = "--backflip"; break;
                            case "outext": argName = "--output-ext"; break;
                            case "basename": argName = "--output-basename"; break;
                            case "rotate": argName = "--rotate"; break;
                            case "crop_left": argName = "--crop-left"; break;
                            case "crop_right": argName = "--crop-right"; break;
                            case "crop_top": argName = "--crop-top"; break;
                            case "crop_bottom": argName = "--crop-bottom"; break;
                        }
                        if (argName != null && !String.IsNullOrEmpty(settingValue))
                        {
                            try
                            {
                                settings = FromArgs(new[] { argName, settingValue }, true, settings);
                            }
                            catch (ArgumentException) { /* Ignore errors */ }
                        }
                    }
                }
            }
            return settings;
        }

        /// <summary>
        /// Write settings to a Batch file.
        /// The batch file can launch processing without using the GUI.
        /// </summary>
        /// <param name="fileName">Name of the Batch file</param>
        /// <param name="settings">Settings to write to the Batch file</param>
        public static void WriteBatchFile(string fileName, Settings settings)
        {
            string batchFileTemplate = "@echo off\r\n"
                + "\r\n"
                + ":: Generated by {0} {1}\r\n"
                + "\r\n"
                + ":: Input configuration\r\n"
                + "\r\n"
                + "set INDIR={2}\r\n"
                + "set INPAGING={3}\r\n"
                + "set INEXT={4}\r\n"
                + "\r\n"
                + ":: Output configuration\r\n"
                + "\r\n"
                + "set OUTDIR={5}\r\n"
                + "set REORDER={6}\r\n"
                + "set BACKFLIP={7}\r\n"
                + "set OUTEXT={8}\r\n"
                + "set BASENAME={9}\r\n"
                + "\r\n"
                + ":: Image transformation\r\n"
                + "\r\n"
                + "set ROTATE={10}\r\n"
                + "set CROP_LEFT={11}\r\n"
                + "set CROP_RIGHT={12}\r\n"
                + "set CROP_TOP={13}\r\n"
                + "set CROP_BOTTOM={14}\r\n"
                + "\r\n"
                + ":: Launch post processing\r\n"
                + "\r\n"
                + "{15} ^\r\n"
                + " --input-dir \"%INDIR%\" ^\r\n"
                + " --input-paging \"%INPAGING%\" ^\r\n"
                + " --input-ext \"%INEXT%\" ^\r\n"
                + " --output-dir \"%OUTDIR%\" ^\r\n"
                + " --reorder \"%REORDER%\" ^\r\n"
                + " --backflip \"%BACKFLIP%\" ^\r\n"
                + " --output-ext \"%OUTEXT%\" ^\r\n"
                + " --output-basename \"%BASENAME%\" ^\r\n"
                + " --rotate \"%ROTATE%\" ^\r\n"
                + " --crop-left \"%CROP_LEFT%\" ^\r\n"
                + " --crop-right \"%CROP_RIGHT%\" ^\r\n"
                + " --crop-top \"%CROP_TOP%\" ^\r\n"
                + " --crop-bottom \"%CROP_BOTTOM%\"\r\n"
                + "\r\n"
                + "pause\r\n";

            File.WriteAllText(fileName,
                String.Format(batchFileTemplate,
                    Program.Name,
                    Program.Version,
                    settings.InputDir,
                    settings.InputPaging,
                    settings.InputExt,
                    settings.OutputDir,
                    settings.Reorder.ToString().ToLower(),
                    settings.Backflip.ToString().ToLower(),
                    settings.OutputExt,
                    settings.OutputPrefix + settings.OutputBasenum.ToString(settings.OutputNumFormat),
                    settings.RotateAngle,
                    settings.CropLeft,
                    settings.CropRight,
                    settings.CropTop,
                    settings.CropBottom,
                    Program.ExeName
                )
            );
        }

        /// <summary>
        /// Get or Set input paging as string
        /// </summary>
        public string InputPaging
        {
            get
            {
                string paging = "single";
                if (DoublePaging)
                {
                    paging = "double";
                    if (PagingRightToLeft)
                        paging = "double-rtl";
                }
                return paging;
            }
            set
            {
                switch (value)
                {
                    case "single":
                        DoublePaging = false;
                        PagingRightToLeft = false;
                        break;
                    case "double":
                        DoublePaging = true;
                        PagingRightToLeft = false;
                        break;
                    case "double-rtl":
                        DoublePaging = true;
                        PagingRightToLeft = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Get or Set rotate angle as string
        /// </summary>
        public string RotateAngle
        {
            get
            {
                switch (Rotate)
                {
                    case RotateFlipType.Rotate90FlipNone: return "90";
                    case RotateFlipType.Rotate180FlipNone: return "180";
                    case RotateFlipType.Rotate270FlipNone: return "270";
                    default: return "0";
                }
            }
            set
            {
                switch (value)
                {
                    case "90": Rotate = RotateFlipType.Rotate90FlipNone; break;
                    case "180": Rotate = RotateFlipType.Rotate180FlipNone; break;
                    case "270": Rotate = RotateFlipType.Rotate270FlipNone; break;
                    default: Rotate = RotateFlipType.RotateNoneFlipNone; break;
                }
            }
        }

        /// <summary>
        /// Get or OutputPrefix and OutputNumFormat as a single string
        /// </summary>
        public string OutputNaming
        {
            get
            {
                return OutputPrefix + OutputBasenum.ToString(OutputNumFormat);
            }
            set
            {
                char[] invalidFnameChars = Path.GetInvalidFileNameChars();
                foreach (char c in value)
                    if (invalidFnameChars.Contains(c))
                        throw new ArgumentException(String.Format(Translations.Get("gui_error_outname"), value));
                char[] baseNumChars = value.Reverse().TakeWhile(c => c >= '0' && c <= '9').Reverse().ToArray();
                OutputPrefix = value.Substring(0, value.Length - baseNumChars.Length);
                if (baseNumChars.Length > 0)
                {
                    OutputBasenum = uint.Parse(new String(baseNumChars));
                    for (int i = 0; i < baseNumChars.Length; i++)
                        baseNumChars[i] = '0';
                    OutputNumFormat = new String(baseNumChars);
                }
            }
        }
    }
}
