using System.IO;
using System.Xml.Linq;
using Xunit;

namespace AndroidXml.Tests
{
    public class AndroidXmlReaderTests
    {
        [Fact]
        public void ReadManifestTest()
        {
            using (Stream stream = File.OpenRead(@"ApiDemos.AndroidManifest.xml"))
            {
                AndroidXmlReader reader = new AndroidXmlReader(stream);
                reader.MoveToContent();
                XDocument document = XDocument.Load(reader);
            }
        }
    }
}
