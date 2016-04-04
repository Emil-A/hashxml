/*
* This file should be compiled to an executable and ran to create an executable to run.
* The executable stores hashes of all files scraped from the links into temp.xml 
*/
using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public static class UpdateXml
{
	public static int getHash(string str)
	{
		// function borrowed from http://stackoverflow.com/questions/7616461/generate-a-hash-from-string-in-javascript-jquery
		int hash = 0;
		int chr; 
		int len = str.Length;
		if (len == 0) return hash;
		for (int i = 0; i < len; i++) {
			chr   = (int)str[i];

			// Below:
			// first, add five 0 to the end of the character.
			// So, if chr = 5, then it can represented as 101.  Then it becomes 10100000 (minus 0 (when i == 0)) + 101 = 10100101
			hash  = ((hash << 5) - hash) + chr;

			// Below:
			// Convert to 32bit integer, it's a bitwise OR assignement operator, hash = hash | 0;
			// So, if the hash happens to be 5, then has equals 00000000000000000000000000000101.
			hash |= 0;
		}
		return hash;
	}
	
	public static void writeHash(string theme, string path)
	{
		string fileText;
		string fileName;
		string dirName;
		string fileLocation = "./temp.xml";
		int hash;
		List<string> links = new List<string>();
		//Versions 15+
		links.Add(path+"/"+theme+"/css/theme.min.css");
		links.Add(path+"/wet-boew/js/wet-boew.min.js");
		links.Add(path+"/"+theme+"/js/theme.min.js");
		links.Add(path+"/wet-boew/js/polyfills/details.min.js");
		links.Add(path+"/wet-boew/css/polyfills/details.min.css");
		links.Add(path+"/wet-boew/js/polyfills/datepicker.min.js");
		links.Add(path+"/wet-boew/css/polyfills/datepicker.min.css");
		links.Add(path+"/wet-boew/js/i18n/en.min.js");
		links.Add(path+"/"+theme+"/css/wet-boew.min.css");//only found in canada.ca
		links.Add(path+"/"+theme+"/css/theme.css");//only found in canada.ca
		
		//Versions 14-
		/*links.Add(path+"/css/theme.min.css");
		links.Add(path+"/js/wet-boew.min.js");
		links.Add(path+"/js/theme.min.js");//not in v4.0.6 gcweb or below, not in v4.0.4 intranet below, not v4.0.3 ut below
		links.Add(path+"/js/polyfills/details.min.js");
		links.Add(path+"/css/polyfills/details.min.css");
		links.Add(path+"/js/polyfills/datepicker.min.js");
		links.Add(path+"/css/polyfills/datepicker.min.css");
		links.Add(path+"/js/i18n/en.min.js");
		links.Add(path+"/css/wet-boew.min.css");//only found in canada.ca*/

		//Unnecessary for older versions
		/*if(theme == "GCWeb") {
			links.Add(path+"/"+theme+"/css/social-media-centre.min.css");//only found in canada.ca, only in gcweb
			links.Add(path+"/"+theme+"/css/mobile-centre.min.css");//only found in canada.ca, only in gcweb
		}*/
	
		//Check:
		//util-min.css
		//pe-ap-min.css
		//theme-min.css
		//en-min.js
		//settings,js
		//theme-min.js
		//pe-ap-min.js
		dirName = Path.GetFileName(path);
		Console.WriteLine(); 
		
		
		//Have to create new document because other XML readers require Linq libraries 
		//This is to format XML with white space
		XmlWriterSettings settings = new XmlWriterSettings();
		settings.Indent = true;
		settings.IndentChars = "\t";
		XmlWriter xmlWriter = XmlWriter.Create(fileLocation, settings);		

		xmlWriter.WriteStartDocument();
		xmlWriter.WriteStartElement("widgets");

		xmlWriter.WriteStartElement("dir");
		xmlWriter.WriteAttributeString("name", dirName);
		
		foreach(string link in links){
			fileText = File.ReadAllText(@link);
			fileName = Path.GetFileName(link);
			hash = UpdateXml.getHash(fileText);
			
			xmlWriter.WriteStartElement("file");
			xmlWriter.WriteAttributeString("name", fileName);
			xmlWriter.WriteAttributeString("hash", hash.ToString());
			xmlWriter.WriteEndElement();
			
			Console.WriteLine(fileName + ": " + hash);
		}	
		xmlWriter.WriteEndElement();
		xmlWriter.WriteEndDocument();
		xmlWriter.Close();
	}

}

class RunUpdate
{

	static void Main()
	{
		//This is pretty inefficient, probably better to take in command line args and run each of theese 
		string path; 		
		path = @"C:\Projects\hashxml\cdn_folders\themes-dist-4.0.21-gcweb\themes-dist-4.0.21-gcweb";
		UpdateXml.writeHash("GCWeb", path);

		//path = @"C:\Projects\hashxml\cdn_folders\themes-dist-4.0.21-theme-gc-intranet\themes-dist-4.0.21-theme-gc-intranet";
		//UpdateXml.writeHash("theme-gc-intranet", path);
		
		//path = @"C:\Projects\hashxml\cdn_folders\themes-dist-4.0.21-theme-gcwu-fegc\themes-dist-4.0.21-theme-gcwu-fegc";
		//UpdateXml.writeHash("theme-gcwu-fegc", path);
		

	}
}