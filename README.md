# hashxml
Just hosting some XML needed for an extension at work 

You should fork or clone this repository at https://github.com/Emil-A/hashxml to make changes.

To run this just run the executable compiled from the c# file. This will generate an XML file with the desire contents for the version/theme.
Paste the contents of the generated file [(temp.xml)](temp.xml) into the main XML file [(wetHashFiles.xml)](wetHashFiles.xml) storing all the file hash information, wherever it is hosted.

Make sure to store the files to store the files for a folder/theme in a parent directory: cdn_folders

All versions of WET 4.0+ for canda.ca, utweb, intranet themes are stored in wetHashFiles.xml.
It will need to be updated with new versions every couple of months, stable releases can be seen here:
http://wet-boew.github.io/wet-boew/docs/versions/dwnld-en.html

ex: v4.0.20 GCWeb Theme
![extension screenshot](https://raw.githubusercontent.com/Emil-A/hashxml/master/images/screenshot.png)

*The process could be improved to be more efficient by taking in what version/type of XML to generate in command line arguments, never got around to it.
