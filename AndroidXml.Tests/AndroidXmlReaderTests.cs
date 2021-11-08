// Copyright (c) 2015 Quamotion
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace AndroidXml.Tests
{
    public class AndroidXmlReaderTests
    {
        [Theory]
        [InlineData("ApiDemos.AndroidManifest.xml")]
        [InlineData("AndroidManifest2.xml")]
        public void ReadManifestTest(string path)
        {
            using (Stream stream = File.OpenRead(path))
            {
                AndroidXmlReader reader = new AndroidXmlReader(stream);
                reader.MoveToContent();
                XDocument document = XDocument.Load(reader);
            }
        }

        [Theory]
        [InlineData("ApiDemos.AndroidManifest.xml")]
        [InlineData("AndroidManifest2.xml")]
        public async Task ReadManifestAsyncTest(string path)
        {
            using (Stream stream = File.OpenRead(path))
            {
                AndroidXmlReader reader = new AndroidXmlReader(stream);
                await reader.MoveToContentAsync();
                XDocument document = XDocument.Load(reader);
            }
        }
    }
}
