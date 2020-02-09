==================================================================
==== Paper Stack Processor v1.2.0 - Par ORelio - Microzoom.fr ====
==================================================================

Merci d'avoir téléchargé Paper Stack Processor !

Ce programme automatise le traitement par lot du scan d'une pile de papier en combinant :

 - Rotation
 - Rognage
 - Découpage
 - Réordonnancement

Bien qu'il puisse servir pour d'autres besoins, son cas d'usage typique est en conjonction avec
un scanneur à défilement simple, qui prend les feuilles de papier dans une fente d'alimentation
et scanne le recto de chaque page. Ce type de scanneur existe seul ou en imprimante multifunction.

Paper Stack Processor traite les images brutes du scanner pour produire des pages distinctes,
dans le bon ordre. Pour scanner un magazine, il vous faudra sûrement un scanneur A3.

=======================
 Example d'utilisation
=======================

Exemple en prenant un magazine dégrafé :

1) Scan des pages recto, en plaçant le magazine dans l'alimentation papier

   Lors du scan, réglez la taille du papier sur A3 au lieu d'utiliser l'autodétection,
   et assurez-vous que le format de scan est réglé sur JPEG car Paper Stack Processor
   ne gère pas les images au format PDF.
   Répétez l'opération une seconde fois pour le verso de votre pile de papier.
   Placez toutes les images résultantes dans un dossier vide.

2) Traitement du dossier d'Entrée dans Paper Stack Processor

   Sélectionner le dossier créé à l'étape 1) comme dossier d'Entrée.
   Réglez la rotation si nécessaire afin que l'image soit à l'endroit.
   Sélectionnez les 4 coins du magazine en cliquant dessus avec la souris.
   Ajustez la pagination d'entrée sur le réglage "double".
   Cliquez sur Démarrer le traitement
   Paper Stack Processor va s'occuper du rognage, découpage et réordonnancement,
   puis générer les pages au propre dans des fichiers PNG.

Le même processus peut s'appliquer sur une pile simple de feuilles A4,
la seule différence étant le réglage de la pagination d'entrée sur "single".

Après traitement, vous voudrez sûrement optimiser la taille de vos images en diminuant
leur définition et en les recompressant en JPEG. Poru cela, vous pouvez utiliser le mode de
traitement par lot de RIOT - Radical Image Optimization Tool : https://riot-optimizer.com/

Vous pouvez enfin zipper les images et renommer l'archive '.zip' en '.cbz', pour pouvoir
lire facilement vos scans via des programmes comme CDisplayEx : https://www.cdisplayex.com/

==================================
 Utilisation en ligne de commande
==================================

Lors de la configuration depuis l'interface graphique, un fichier Batch est généré.
Le traitement peut être lancé directement en ouvrant le fichier Batch.
Lors du lancement de l'interface, les réglages sont récupérés dans le fichier Batch.

Si vous utilisez Mac ou Linux, vous préférerez peut-être utiliser la ligne de commande :

$ mono PostProcess.exe

== Argument         Par défaut     Description ==
 --input-dir        (requis)       Dossier d'entrée dans lequel sont lues les images
 --output-dir       (requis)       Dossier de sortie dans lequel sont écrites les images
 --rotate           0              Rotation des images en entrée de 0, 90, 180 et 270 degrés
 --crop-left        0              Après rotation, retirer des colonnes de pixels sur la gauche
 --crop-right       0              Après rotation, retirer des colonnes de pixels sur la droite
 --crop-top         0              Après rotation, retirer des lignes de pixels en haut
 --crop-bottom      0              Après rotation, retirer des lignes de pixels en bas
 --input-ext        jpg            Type des images en entrés. Sont pris en charge: jpg, jpeg, png, bmp
 --input-paging     single         'double'/'double-rtl': Séparer les pages en 2 après rotation/rognage
 --output-ext       png            Type des images en sortie. Sont pris en charge: jpg, jpeg, png, bmp
 --output-basename  IMG0001        Motif de nommage des images en sorties, le nombre doit être à la fin
 --reorder          true           Réorganiser les pages depuis l'entrée en moitié-recto, moitié-verso
 --backflip         false          Gérer la seconde moitié de l'entrée placée à l'envers
 --gui              (none)         Démarrer en interface graphique et y charger les autres arguments

Sous Windows, l'action par défaut sans arguments est de démarrer en interface graphique.
Utilisez `--help` pour afficher à la place l'aide en ligne de commande.

=========
 Crédits
=========

L'icône suivante est utilisée au sein de Paper Stack Processor :

 - Normal Paper Box Icon par Lokas Software (https://www.awicons.com/)

Source : http://www.iconarchive.com/show/vista-artistic-icons-by-awicons.html
Cette icône est licensiée sous Creative Commons Attribution 3.0 Unported.

+--------------------+
| © 2018-2020 ORelio |
+--------------------+