var fs = require('fs');
var DOMParser = require('xmldom').DOMParser;
var parser = new DOMParser();
//var xmlDoc=parser.parseFromString("wetHashFiles.xml","text/xml");

var theme, link;
var links = [
"http://wet-boew.github.io/themes-dist/"+theme+"/"+theme+"/css/theme.min.css",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/js/wet-boew.min.js",
"http://wet-boew.github.io/themes-dist/"+theme+"/"+theme+"/js/theme.min.js",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/js/polyfills/details.min.js",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/css/polyfills/details.min.css",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/js/polyfills/datepicker.min.js",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/css/polyfills/datepicker.min.css",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/js/i18n/en.min.js",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/js/polyfills/details.min.js",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/css/polyfills/details.min.css",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/js/polyfills/datepicker.min.js",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/css/polyfills/datepicker.min.css",
"http://wet-boew.github.io/themes-dist/"+theme+"/wet-boew/js/i18n/en.min.js"];

var getRemoteFile = function (file, callback) {
	var xmlhttp=new XMLHttpRequest();
	try {
		if (file.match(/^c:/i)) {
			file = file.replace(/\\/g, "/");
			file = "file:///" + file;
		}
	
		xmlhttp.open("GET",file,true);
		xmlhttp.onreadystatechange=function () {
			if (xmlhttp.readyState == 4) {
				if (xmlhttp.status == 404) {
					callback("404");
				}
				if (xmlhttp.status == 0 || xmlhttp.status == 200) callback(xmlhttp.responseText);
			}
		}
		xmlhttp.send();
	}
	catch (ex) {
		callback(null);
	}
} // end of getRemoteFile

var getHash = function (str) {
	// function borrowed from http://stackoverflow.com/questions/7616461/generate-a-hash-from-string-in-javascript-jquery
	var hash = 0, i, chr, len = str.length;
	if (len == 0) return hash;
	for (i = 0; i < len; i++) {
		chr   = str.charCodeAt(i);

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
} // End of getHash

var writeHashes = function (theme) {
	newDir = doc.createElement('dir');
	newDir.setAttribute("name", theme+"-4.0.19");
	root.appendChild(newDir);
	
	for (var i = 0; i < links.length; i++) {
		link = links[i];
		getRemoteFile (url, function (link) {
			if (links[i] == "404") {
				showError();
				if (callback != undefined) callback(false);
			//no issues getHash of the links[i] page content
			} else {
				newFile = doc.createElement('file');
				//set attribute name to filename of link and hash to hash returned
				newFile.setAttribute("name", links[i].split('/').pop());
				newFile.setAttribute("hash",getHash(links[i]));
				newDir.appendChild(newFile);
			}
		});
	}
} // End of writeHashes


var doc = new DOMParser().parseFromString( '<widgets lsr="4.0.13" ldr ="4.0.0-rc1">\n'+'</widgets>','text/xml');
var root = doc.getElementsByTagName('widgets');
var  newDir, newFile;

//add gcweb/canada.ca theme hashes
theme = "GCWeb"
writeHashes(theme);
//add intranet theme hashes
theme = "theme-gcwu-fegc"
writeHashes(theme);
//add usability theme hashes
theme = "theme-gc-intranet"
writeHashes(theme);


