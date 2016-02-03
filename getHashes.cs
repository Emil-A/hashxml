using System;
using System.Xml;
using System.IO;

public static class UpdateXml
{
	public static void getFiles(string path, string theme)
	{
		string fileText;
		string[] links = new string[13] {
		path+"/"+theme+"/css/theme.min.css",
		path+"/wet-boew/js/wet-boew.min.js",
		path+"/"+theme+"/js/theme.min.js",
		path+"/wet-boew/js/polyfills/details.min.js",
		path+"/wet-boew/css/polyfills/details.min.css",
		path+"/wet-boew/js/polyfills/datepicker.min.js",
		path+"/wet-boew/css/polyfills/datepicker.min.css",
		path+"/wet-boew/js/i18n/en.min.js",
		path+"/wet-boew/js/polyfills/details.min.js",
		path+"/wet-boew/css/polyfills/details.min.css",
		path+"/wet-boew/js/polyfills/datepicker.min.js",
		path+"/wet-boew/css/polyfills/datepicker.min.css",
		path+"/wet-boew/js/i18n/en.min.js"};
		
		 fileText = File.ReadAllText(@"");
	}
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
		int hash;
		string[] links = new string[8] {
		path+"/"+theme+"/css/theme.min.css",
		path+"/wet-boew/js/wet-boew.min.js",
		path+"/"+theme+"/js/theme.min.js",
		path+"/wet-boew/js/polyfills/details.min.js",
		path+"/wet-boew/css/polyfills/details.min.css",
		path+"/wet-boew/js/polyfills/datepicker.min.js",
		path+"/wet-boew/css/polyfills/datepicker.min.css",
		path+"/wet-boew/js/i18n/en.min.js"};
		
		
		dirName = Path.GetFileName(path);
		Console.WriteLine(); 
		XmlDocument doc = new XmlDocument();
		
		XmlWriter xmlWriter = XmlWriter.Create("./test.xml");

		xmlWriter.WriteStartDocument();
		xmlWriter.WriteStartElement("widgets");

		xmlWriter.WriteStartElement("dir");
		xmlWriter.WriteAttributeString("name", dirName);
		
		for(int i=0; i < links.Length; i++){
			fileText = File.ReadAllText(@links[i]);
			fileName = Path.GetFileName(links[i]);
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
		string path; 		
		path = @"C:\Projects\hashxml\cdn_folders\themes-dist-4.0.16-gcweb\themes-dist-4.0.16-gcweb";
		UpdateXml.writeHash("GCWeb", path);

		//path = @"C:\Projects\hashxml\cdn_folders\themes-dist-4.0.16-theme-gc-intranet\themes-dist-4.0.16-theme-gc-intranet";
		//UpdateXml.writeHash("theme-gc-intranet", path);
		
		//path = @"C:\Projects\hashxml\cdn_folders\themes-dist-4.0.16-theme-gcwu-fegc\themes-dist-4.0.16-theme-gcwu-fegc";
		//UpdateXml.writeHash("theme-gcwu-fegc", path);
		

	}
}