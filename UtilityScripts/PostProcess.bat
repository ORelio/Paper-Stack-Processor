@echo off

:: input configuration

set INDIR=input
set INPAGING=double
set INEXT=jpg

:: output configuration

set OUTDIR=processed
set REORDER=true
set BACKFLIP=false
set OUTEXT=png
set BASENAME=IMG0001

:: image transformation

set ROTATE=90
set CROP_LEFT=0
set CROP_RIGHT=100
set CROP_TOP=200
set CROP_BOTTOM=100

:: launch post processing

PostProcess ^
 --input-dir "%INDIR%" ^
 --input-paging "%INPAGING%" ^
 --input-ext "%INEXT%" ^
 --output-dir "%OUTDIR%" ^
 --reorder "%REORDER%" ^
 --backflip "%BACKFLIP%" ^
 --output-ext "%OUTEXT%" ^
 --output-basename "%BASENAME%" ^
 --rotate "%ROTATE%" ^
 --crop-left "%CROP_LEFT%" ^
 --crop-right "%CROP_RIGHT%" ^
 --crop-top "%CROP_TOP%" ^
 --crop-bottom "%CROP_BOTTOM%"

pause