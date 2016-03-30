# hashxml
Just hosting some XML needed for an extension at work 

You should fork or clone this repository at https://github.com/Emil-A/hashxml to make changes.

To run this just run the executable compiled from the c# file. This will generate an XML file with the desire contents for the version/theme.
Paste the contents of the generated file into the main XML file storing all the file hash information, wherever it is hosted.

All versions of WET 4.0+ for canda.ca, utweb, intranet themes are stored in wetHashFiles.xml.
It will need to be updated with new versions every couple of months, stable releases can be seen here:
http://wet-boew.github.io/wet-boew/docs/versions/dwnld-en.html

*The process could be improved to be more efficient by taking in what version/type of XML to generate in command line arguments, never got around to it.
