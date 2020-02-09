=================================================================
==== Paper Stack Processor v1.2.0 - By ORelio - Microzoom.fr ====
=================================================================

Thanks for dowloading Paper Stack Processor!

This program automates the processing of paper stack scans by combining basic batch operations:

 - Rotate
 - Crop
 - Split
 - Reorder

Although it can serve other needs, its typical use case is in conjunction with a single-sided
ADF scanner, which takes paper sheets using a paper feeder and scans the front side of each
page. This type of scanner is typically found standalone or as part of a multifunction printer.

Paper Stack Processor will process raw images from the scanner into single pages in
correct order. For scanning magazines, you'll likely need a scanner supporting A3 paper.

===============
 Usage Example
===============

Example using an unstapled magazine :

1) Scan all front pages (recto), by placing the stack in the paper feeder

   When scanning, set the input size to A3 instead of autodetect, and make sure
   scan format is set to JPEG as Paper Stack Processor does not support PDF.
   Do the same operation another time to scan the verso of your paper stack.
   Place all the resulting images from the scanner in an empty folder.

2) Process your Input folder in Paper Stack Processor

   Select the folder created in step 1) as Input folder in Paper Stack Processor.
   Rotate the input image if needed. Select all 4 corners of the magazine,
   then adjust input paging to 'double' and click 'Launch processing'.
   Paper Stack Processor will handle the croping, splitting and reordering,
   then output single pages as PNG files.

The same process can apply to simple A4 paper stacks, the only
difference being that Input paging must be set to 'single' in GUI.

Afterwards, you'll likely want to optimize image size by reducing resolution
and compressing to JPEG. For that purpose, you can use the batch mode of
RIOT - Radical Image Optimization Tool : https://riot-optimizer.com/

You can zip the images and rename your '.zip' archive to '.cbz', then conveniently
read your scanned magazine using programs such as CDisplayEx : https://www.cdisplayex.com/

====================
 Command-line usage
====================

When configuring the GUI, a Batch file is generated.
Processing can be launched directly using the Batch file without opening the GUI.
When launching the GUI, settings are read back from the Batch file.

If running Mac or Linux, you might prefer the command-line usage:

$ mono PostProcess.exe

== Argument Name    Default        Description ==
 --input-dir        (mandatory)    Input directory to read images from
 --output-dir       (mandatory)    Output directory to write images to
 --rotate           0              Rotate input images by 0, 90, 180 or 270 degrees
 --crop-left        0              After rotating, remove pixel columns on the left
 --crop-right       0              After rotating, remove pixel columns on the right
 --crop-top         0              After rotating, remove pixel lines on the top
 --crop-bottom      0              After rotating, remove pixel lines on the bottom
 --input-ext        jpg            Input image file type, supports jpg, jpeg, png, bmp
 --input-paging     single         'double'/'double-rtl': Split pages in 2 after rotate/crop
 --output-ext       png            Output image file type, supports jpg, jpeg, png, bmp
 --output-basename  IMG0001        Base naming of output files, number must be placed last
 --reorder          true           Reorder pages, assuming input is all front then all back
 --backflip         false          Handle second half of the input stack placed upside down
 --gui              (none)         Launch GUI and set other arguments in GUI settings

On Windows, the default action without argument is to launch the GUI.
Use `--help` for displaying command-line help instead.

=========
 Credits
=========

The following icon is used within Paper Stack Processor:

 - Normal Paper Box Icon by Lokas Software (https://www.awicons.com/)

Source: http://www.iconarchive.com/show/vista-artistic-icons-by-awicons.html
This icon is licensed under Creative Commons Attribution 3.0 Unported.

+--------------------+
| © 2018-2020 ORelio |
+--------------------+