| build | license |
|-------|---------|

| [![Build Status](https://ci.appveyor.com/api/projects/status/github/quamotion/androidxmldotnet)] | [![License](https://img.shields.io/github/license/mashape/apistatus.svg)](http://opensource.org/licenses/MIT)

# androidxmldotnet
## Project Description
A library for parsing Android binary XML format. You could use it to parse `AndroidManifest.xml` inside the APK files.

## Overview
The library implements three interfaces for reading Android binary XML files:

* `AndroidXmlReader` implements `System.Xml.XmlReader` for compatibility with other XML libraries. 
* `ResReader` can be used to read the basic chunks of the file. This is used by `AndroidXmlReader`. 
* `ResWriter` can be used to write the basic chunks of the file.

## Example

```
var reader = new AndroidXmlReader(stream);
XDocument doc = XDocument.Load(reader);
```

## Useful links

* [Just an application: Android Internals: Binary XML](https://justanapplication.wordpress.com/2011/09/22/android-internals-binary-xml-part-two-the-xml-chunk/) - A description of the format used.

## Notes
This project was forked from http://androidxmldotnet.codeplex.com/
