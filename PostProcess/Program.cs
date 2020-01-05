using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using SharpTools;

namespace PostProcess
{
    /// <summary>
    /// Paper Stack Processor - By ORelio (c) 2018-2020 - CDDL 1.0
    /// </summary>
    class Program
    {
        public const string Name = "Paper Stack Processor";
        public const string Version = "v1.2.0";
        public const string Title = Program.Name + " " + Program.Version + " - by ORelio - MicroZOOM.fr";
        public static readonly string ExeName = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string BatName = Path.GetFileNameWithoutExtension(ExeName) + ".bat";

        [STAThread]
        static void Main(string[] args)
        {
            if ((args.Length == 0 && !IsUsingMono) || args.Contains("--gui"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    Settings settings = Settings.FromBatchFile(Program.BatName);
                    settings = Settings.FromArgs(args, true, settings);
                    Application.Run(new FormMain(settings));
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
            else
            {
                BindToConsole();

                if (args.Length > 0 && !args.Contains("--help"))
                {
                    try
                    {
                        ImageProcessor.Run(Settings.FromArgs(args),
                            (output) => Console.WriteLine(output),
                            (error) =>
                            {
                                Console.Error.WriteLine(String.Format(Translations.Get("program_error"), error));
                                Environment.Exit(1);
                            }
                        );
                    }
                    catch (ArgumentException e)
                    {
                        Console.Error.WriteLine(String.Format(Translations.Get("program_error"), e.Message));
                        Console.Error.WriteLine(String.Format(Translations.Get("help_usage_detailed"), Program.ExeName));
                        Environment.Exit(1);
                    }
                }
                else
                {
                    if (args.Contains("--help"))
                    {
                        foreach (string line in new[]{
                            "== " + Program.Title + " ==",
                            " " + Translations.Get("help_main_1"),
                            " " + Translations.Get("help_main_2"),
                            "",
                            "== " + Translations.Get("help_title_usage") + " ==",
                            " " + Translations.Get("help_steps_1"),
                            " " + String.Format(Translations.Get("help_steps_2"), Program.ExeName),
                            " " + Translations.Get("help_steps_3"),
                            "",
                            "== " + Translations.Get("help_title_args") + " ==",
                            " " + Translations.Get("help_arg_indir"),
                            " " + Translations.Get("help_arg_outdir"),
                            " " + Translations.Get("help_arg_rotate"),
                            " " + Translations.Get("help_arg_crop_left"),
                            " " + Translations.Get("help_arg_crop_right"),
                            " " + Translations.Get("help_arg_crop_top"),
                            " " + Translations.Get("help_arg_crop_bottom"),
                            " " + Translations.Get("help_arg_inext"),
                            " " + Translations.Get("help_arg_inpaging"),
                            " " + Translations.Get("help_arg_outext"),
                            " " + Translations.Get("help_arg_basename"),
                            " " + Translations.Get("help_arg_reorder"),
                            " " + Translations.Get("help_arg_backflip"),
                            " " + Translations.Get("help_arg_gui"),
                            ""
                        })
                        {
                            Console.Error.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine(String.Format(Translations.Get("help_usage_example"), Program.ExeName));
                        Console.Error.WriteLine(String.Format(Translations.Get("help_usage_detailed"), Program.ExeName));
                    }
                }
            }
        }

        /// <summary>
        /// Detect if the user is running the application through Mono
        /// </summary>
        public static bool IsUsingMono
        {
            get
            {
                return Type.GetType("Mono.Runtime") != null;
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// On Windows, console will not show anything unless a system call is made
        /// </summary>
        public static void BindToConsole()
        {
            if (!IsUsingMono)
            {
                AttachConsole(ATTACH_PARENT_PROCESS);
            }
        }
    }
}
