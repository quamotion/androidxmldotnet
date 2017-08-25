using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Linq;

namespace AndroidXml.Tests
{
    [TestClass]
    public class AndroidXmlReaderTests
    {
        [TestMethod]
        [DeploymentItem("ApiDemos.AndroidManifest.xml")]
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
