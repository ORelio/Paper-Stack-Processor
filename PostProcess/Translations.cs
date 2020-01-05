using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SharpTools
{
    /// <summary>
    /// Allows to localize the app in different languages
    /// </summary>
    /// <remarks>
    /// By ORelio (c) 2015-2018 - CDDL 1.0
    /// </remarks>
    public static class Translations
    {
        private static Dictionary<string, string> translations;

        /// <summary>
        /// Return a tranlation for the requested text
        /// </summary>
        /// <param name="msg_name">text identifier</param>
        /// <returns>returns translation for this identifier</returns>
        public static string Get(string msg_name)
        {
            if (translations.ContainsKey(msg_name))
                return translations[msg_name];

            return msg_name.ToUpper();
        }

        /// <summary>
        /// Initialize translations depending on system language.
        /// English is the default for all unknown system languages.
        /// </summary>
        static Translations()
        {
            translations = new Dictionary<string, string>();

            /*
             * External translation files
             * These files are loaded from the installation directory as:
             * Lang/abc.ini, e.g. Lang/eng.ini which is the default language file
             * Useful for adding new translations of fixing typos without recompiling
             */

            string systemLanguage = CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;
            string langDir = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Lang" + Path.DirectorySeparatorChar;
            string langFileSystemLanguage = langDir + systemLanguage + ".ini";
            string langFile = File.Exists(langFileSystemLanguage) ? langFileSystemLanguage : langDir + "eng.ini";

            if (File.Exists(langFile))
            {
                foreach (string lineRaw in File.ReadAllLines(langFile, Encoding.UTF8))
                {
                    //This only handles a subset of the INI format:
                    //key=value pairs, no sections, no inline comments.
                    string line = lineRaw.Trim();
                    string translationName = line.Split('=')[0];
                    if (line.Length > (translationName.Length + 1))
                    {
                        string translationValue = line.Substring(translationName.Length + 1);
                        translations[translationName] = translationValue;
                    }
                }
            }

            /* 
             * Hardcoded translation data
             * This data is used as fallback if no translation file could be loaded
             * Useful for standalone exe portable apps
             */

            else if (systemLanguage == "fra")
            {
                translations["program_error"] = "Erreur: {0}";
                translations["argument_error_rotate"] = "Valeur de --rotate non supportée. Valeurs possibles : 0, 90, 180 ou 270.";
                translations["argument_error_crop_left"] = "Valeur invalide pour --crop-left";
                translations["argument_error_crop_right"] = "Valeur invalide pour --crop-right";
                translations["argument_error_crop_top"] = "Valeur invalide pour --crop-top";
                translations["argument_error_crop_bottom"] = "Valeur invalide pour --crop-bottom";
                translations["argument_error_outname"] = "Nom de fichier invalide dans --output-basename";
                translations["argument_error_paging"] = "Mode de mise en page inconnu pour --input-paging. Valeurs possibles : 'single', 'double' ou 'double-rtl'";
                translations["argument_error_reorder"] = "Valeur booléene inconnue pour --reorder, true/false attendu";
                translations["argument_error_backflip"] = "Valeur booléene inconnue pour --backflip, true/false attendu";
                translations["argument_error_unknown_arg"] = "Argument inconnu : {0}";
                translations["argument_error_missing_value"] = "Valeur manquante pour l'argument : {0}";
                translations["argument_error_unknown_positional"] = "Argument positionnel inconnu : {0}";
                translations["argument_error_indir"] = "Missing argument: --input-dir. see --help for usage.";
                translations["argument_error_outdir"] = "Missing argument: --output-dir. see --help for usage.";
                translations["help_main_1"] = "Post-traitement de scans par lot : piles de feuilles simples ou pages de magazine dégrafées";
                translations["help_main_2"] = "Ce programme peut également appliquer par lot des opérations de rotation, rognage, découpage";
                translations["help_title_usage"] = "Mode d'emploi";
                translations["help_steps_1"] = "1. Scanner tous les côtés pile, puis retourner le tas de papier et scanner tous les côtés face";
                translations["help_steps_2"] = "2. Lancer {0} --input-dir <dossier> --output-dir <dossier>";
                translations["help_steps_3"] = "3. Les pages sont remises dans l'ordre et optionellement pivotées, rognées et séparées en 2";
                translations["help_title_args"]      =   "Argument         Par défaut     Description";
                translations["help_arg_indir"]       = "--input-dir        (requis)       Dossier d'entrée dans lequel sont lues les images";
                translations["help_arg_outdir"]      = "--output-dir       (requis)       Dossier de sortie dans lequel sont écrites les images";
                translations["help_arg_rotate"]      = "--rotate           0              Rotation des images en entrée de 0, 90, 180 et 270 degrés";
                translations["help_arg_crop_left"]   = "--crop-left        0              Après rotation, retirer des colonnes de pixels sur la gauche";
                translations["help_arg_crop_right"]  = "--crop-right       0              Après rotation, retirer des colonnes de pixels sur la droite";
                translations["help_arg_crop_top"]    = "--crop-top         0              Après rotation, retirer des lignes de pixels en haut";
                translations["help_arg_crop_bottom"] = "--crop-bottom      0              Après rotation, retirer des lignes de pixels en bas";
                translations["help_arg_inext"]       = "--input-ext        jpg            Type des images en entrés. Sont pris en charge: jpg, jpeg, png, bmp";
                translations["help_arg_inpaging"]    = "--input-paging     single         'double'/'double-rtl': Séparer les pages en 2 après rotation/rognage";
                translations["help_arg_outext"]      = "--output-ext       png            Type des images en sortie. Sont pris en charge: jpg, jpeg, png, bmp";
                translations["help_arg_basename"]    = "--output-basename  IMG0001        Motif de nommage des images en sorties, le nombre doit être à la fin";
                translations["help_arg_reorder"]     = "--reorder          true           Réorganiser les pages depuis l'entrée en moitié-recto, moitié-verso";
                translations["help_arg_backflip"]    = "--backflip         false          Gérer la seconde moitié de l'entrée placée à l'envers";
                translations["help_arg_gui"]         = "--gui              (none)         Démarrer en interface graphique et y charger les autres arguments";
                translations["help_usage_example"] = "Utilisation: {0} --input-dir <dossier> --output-dir <dossier>";
                translations["help_usage_detailed"] = "Voir {0} --help pour l'aide détaillée";
                translations["process_error_indir_not_found"] = "Le dossier d'entrée n'existe pas : {0}";
                translations["process_error_no_input_files"] = "Pas de fichiers .{0} dans le dossier d'entrée !";
                translations["process_error_uneven_input_files"] = "Le nombre d'images en entrée étant impair, il n'est pas possible de les réordonner.";
                translations["process_error_invalid_crop_values"] = "Les valeurs de rognage sont plus grandes que la taille de l'image";
                translations["process_status_reading_input_dir"] = "Ouverture du dossier d'entrée";
                translations["process_status_input_dir_count"] = "Le dossier d'entrée contient {0} images";
                translations["process_status_processing_file"] = "Traitement en cours : {0} ({1}/{2})";
                translations["process_status_reordering_files"] = "Réorganisation des fichiers de sortie";
                translations["process_status_renaming_files"] = "Renommage des fichiers de sortie";
                translations["process_status_done"] = "Terminé.";
                translations["gui_group_input_config"] = "Réglages d'Entrée";
                translations["gui_lbl_type"] = "Type:";
                translations["gui_lbl_paging"] = "Pages:";
                translations["gui_lbl_folder"] = "Dossier:";
                translations["gui_group_output_config"] = "Réglages de Sortie";
                translations["gui_lbl_naming"] = "Nom:";
                translations["gui_group_basic_transforms"] = "Rotation et Tri";
                translations["gui_lbl_rotate"] = "Angle:";
                translations["gui_box_reorder"] = "Réordonner recto en recto/verso";
                translations["gui_box_backflip"] = "Pivoter verticalement la 2e moitié";
                translations["gui_group_crop"] = "Rogner l'image";
                translations["gui_lbl_bottom"] = "Bas:";
                translations["gui_lbl_top"] = "Haut:";
                translations["gui_lbl_left"] = "Gauche:";
                translations["gui_lbl_right"] = "Droite:";
                translations["gui_btn_launch"] = "Démarrer le traitement";
                translations["gui_btn_cancel"] = "Annuler le traitement";
                translations["gui_error_occured"] = "Une erreur s'est produite";
                translations["gui_error_outname"] = "Nom de fichier invalide : {0}";
                translations["gui_tooltip_input"] = "Réglages des images en entrée";
                translations["gui_tooltip_indir"] = "Sélectionner le dossier depuis lequel lire les images";
                translations["gui_tooltip_inpaging"] = "single : Une seule page par image\ndouble : Deux pages, gauche d'abord\ndouble-rtl : Deux pages, droite d'abord";
                translations["gui_tooltip_inext"] = "Extension de fichier des images en entrée";
                translations["gui_tooltip_output"] = "Réglages des images en sortie";
                translations["gui_tooltip_outdir"] = "Sélectionner le dossier dans lequel écrire les images traitées";
                translations["gui_tooltip_basename"] = "Nom des fichiers en sortie au format NOM999 où 999 est le nombre de départ";
                translations["gui_tooltip_outext"] = "Extension de fichier des images en sortie";
                translations["gui_tooltip_basic_transforms"] = "Opérations à appliquer sur les images";
                translations["gui_tooltip_rotate"] = "Rotation à appliquer sur les images en entrée";
                translations["gui_tooltip_reorder"] = "Réordonner la pile de pages d'abord scannée en recto puis scannée en verso";
                translations["gui_tooltip_backflip"] = "Gérer le cas dans lequel le verso a été scanné à l'envers par rapport au recto";
                translations["gui_tooltip_crop"] = "Cliquer successivement sur les coins de la page pour calculer la zone à conserver";
                translations["gui_tooltip_corner_top_left"] = "Aperçu du coin supérieur gauche de la sélection. Cliquer pour passer à ce coin.";
                translations["gui_tooltip_corner_top_right"] = "Aperçu du coin supérieur droit de la sélection. Cliquer pour passer à ce coin.";
                translations["gui_tooltip_corner_bottom_left"] = "Aperçu du coin inférieur gauche de la sélection. Cliquer pour passer à ce coin.";
                translations["gui_tooltip_corner_bottom_right"] = "Aperçu du coin inférieur droit de la sélection. Cliquer pour passer à ce coin.";
                translations["gui_tooltip_crop_left"] = "Nombre de pixels à rogner à gauche";
                translations["gui_tooltip_crop_right"] = "Nombre de pixels à rogner à droite";
                translations["gui_tooltip_crop_top"] = "Nombre de pixels à rogner en haut";
                translations["gui_tooltip_crop_bottom"] = "Nombre de pixels à rogner en bas";
                translations["gui_tooltip_launch"] = "Sauvegarder les réglages et lancer le traitement";
                //Ajouter de nouvelles traductions ici
            }
            //Add new languages here as 'else if' blocks
            //English is the default language in 'else' block below
            else
            {
                translations["program_error"] = "Error: {0}";
                translations["argument_error_rotate"] = "Unsupported value for --rotate, expecting 0, 90, 180 or 270.";
                translations["argument_error_crop_left"] = "Invalid value for --crop-left";
                translations["argument_error_crop_right"] = "Invalid value for --crop-right";
                translations["argument_error_crop_top"] = "Invalid value for --crop-top";
                translations["argument_error_crop_bottom"] = "Invalid value for --crop-bottom";
                translations["argument_error_outname"] = "Invalid file name in --output-basename";
                translations["argument_error_paging"] = "Unknown paging mode for --input-paging, expecting 'single', 'double' or 'double-rtl'";
                translations["argument_error_reorder"] = "Unknown boolean value for --reorder, expecting true/false";
                translations["argument_error_backflip"] = "Unknown boolean value for --backflip, expecting true/false";
                translations["argument_error_unknown_arg"] = "Unknown argument: {0}";
                translations["argument_error_missing_value"] = "Missing value for argument: {0}";
                translations["argument_error_unknown_positional"] = "Unknown positional argument: {0}";
                translations["argument_error_indir"] = "Missing argument: --input-dir. see --help for usage.";
                translations["argument_error_outdir"] = "Missing argument: --output-dir. see --help for usage.";
                translations["gui_error_outname"] = "Invalid file name: {0}";
                translations["help_main_1"] = "Post-process batches of automated scans: stacks of single pages or unstapled magazine pages";
                translations["help_main_2"] = "This program can also batch-apply simple image operations such as rotate, crop and split";
                translations["help_title_usage"] = "Usage";
                translations["help_steps_1"] = "1. Scan paper stack front pages, then reverse stack and scan back pages";
                translations["help_steps_2"] = "2. Launch {0} --input-dir <dir> --output-dir <dir>";
                translations["help_steps_3"] = "3. Pages are automatically reordered with optional rotate, crop, split";
                translations["help_title_args"]      =   "Argument Name    Default        Description";
                translations["help_arg_indir"]       = "--input-dir        (mandatory)    Input directory to read images from";
                translations["help_arg_outdir"]      = "--output-dir       (mandatory)    Output directory to write images to";
                translations["help_arg_rotate"]      = "--rotate           0              Rotate input images by 0, 90, 180 or 270 degrees";
                translations["help_arg_crop_left"]   = "--crop-left        0              After rotating, remove pixel columns on the left";
                translations["help_arg_crop_right"]  = "--crop-right       0              After rotating, remove pixel columns on the right";
                translations["help_arg_crop_top"]    = "--crop-top         0              After rotating, remove pixel lines on the top";
                translations["help_arg_crop_bottom"] = "--crop-bottom      0              After rotating, remove pixel lines on the bottom";
                translations["help_arg_inext"]       = "--input-ext        jpg            Input image file type, supports jpg, jpeg, png, bmp";
                translations["help_arg_inpaging"]    = "--input-paging     single         'double'/'double-rtl': Split pages in 2 after rotate/crop";
                translations["help_arg_outext"]      = "--output-ext       png            Output image file type, supports jpg, jpeg, png, bmp";
                translations["help_arg_basename"]    = "--output-basename  IMG0001        Base naming of output files, number must be placed last";
                translations["help_arg_reorder"]     = "--reorder          true           Reorder pages, assuming input is all front then all back";
                translations["help_arg_backflip"]    = "--backflip         false          Handle second half of the input stack placed upside down";
                translations["help_arg_gui"] = "--gui              (none)         Launch GUI and set other arguments in GUI settings";
                translations["help_usage_example"] = "Usage: {0} --input-dir <dir> --output-dir <dir>";
                translations["help_usage_detailed"] = "See {0} --help for detailed usage";
                translations["process_error_indir_not_found"] = "Input directory not found: {0}";
                translations["process_error_no_input_files"] = "No .{0} files in input directory!";
                translations["process_error_uneven_input_files"] = "Input image count is not even, cannot reorder.";
                translations["process_error_invalid_crop_values"] = "Crop values are higher than image size";
                translations["process_status_reading_input_dir"] = "Reading input directory";
                translations["process_status_input_dir_count"] = "Input dir contains {0} images";
                translations["process_status_processing_file"] = "Processing: {0} ({1}/{2})";
                translations["process_status_reordering_files"] = "Reordering output files";
                translations["process_status_renaming_files"] = "Renaming output files";
                translations["process_status_done"] = "Done.";
                translations["gui_group_input_config"] = "Input Configuration";
                translations["gui_lbl_type"] = "Type:";
                translations["gui_lbl_paging"] = "Paging:";
                translations["gui_lbl_folder"] = "Folder:";
                translations["gui_group_output_config"] = "Output Configuration";
                translations["gui_lbl_naming"] = "Naming:";
                translations["gui_group_basic_transforms"] = "Rotate and Reorder";
                translations["gui_lbl_rotate"] = "Rotate:";
                translations["gui_box_reorder"] = "Reorder single-sided to both-sided";
                translations["gui_box_backflip"] = "Rotate second half upside down";
                translations["gui_group_crop"] = "Crop Image";
                translations["gui_lbl_bottom"] = "Bottom:";
                translations["gui_lbl_top"] = "Top:";
                translations["gui_lbl_right"] = "Right:";
                translations["gui_lbl_left"] = "Left:";
                translations["gui_btn_launch"] = "Launch Processing";
                translations["gui_btn_cancel"] = "Cancel Processing";
                translations["gui_error_occured"] = "An error occured";
                translations["gui_tooltip_input"] = "Input images settings";
                translations["gui_tooltip_indir"] = "Select folder to read images from";
                translations["gui_tooltip_inpaging"] = "single: One page per image\ndouble: Two pages, left first\ndouble-rtl: Two pages, right first";
                translations["gui_tooltip_inext"] = "Input images file extension";
                translations["gui_tooltip_output"] = "Output images settings";
                translations["gui_tooltip_outdir"] = "Select folder to write processed images to";
                translations["gui_tooltip_basename"] = "Output file names formatted as NAME999 where 999 is the starting number";
                translations["gui_tooltip_outext"] = "Output images file extension";
                translations["gui_tooltip_basic_transforms"] = "Image processing operations";
                translations["gui_tooltip_rotate"] = "Rotate input images";
                translations["gui_tooltip_reorder"] = "Reorder paper stack scanned as all front sides first then reversed and scanned all back sides";
                translations["gui_tooltip_backflip"] = "Handle case where back sides were scanned upside down compared to front sides";
                translations["gui_tooltip_crop"] = "Successively click on page corners to select an area to keep";
                translations["gui_tooltip_corner_top_left"] = "Preview of selected upper-left corner. Click to move on to this corner.";
                translations["gui_tooltip_corner_top_right"] = "Preview of selected upper-right corner. Click to move on to this corner.";
                translations["gui_tooltip_corner_bottom_left"] = "Preview of selected bottom-left corner. Click to move on to this corner.";
                translations["gui_tooltip_corner_bottom_right"] = "Preview of selected bottom-right corner. Click to move on to this corner.";
                translations["gui_tooltip_crop_left"] = "Pixel amount to crop on the left";
                translations["gui_tooltip_crop_right"] = "Pixel amount to crop on the right";
                translations["gui_tooltip_crop_top"] = "Pixel amount to crop on top";
                translations["gui_tooltip_crop_bottom"] = "Pixel amount to crop on bottom";
                translations["gui_tooltip_launch"] = "Save settings and launch processing";
                //Add new translations here
            }
        }
    }
}