using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Debugger.Launch();

            using (Stream stream = File.OpenRead(@"ApiDemos.AndroidManifest.xml"))
            {
                AndroidXmlReader reader = new AndroidXmlReader(stream);
                reader.MoveToContent();
                XDocument document = XDocument.Load(reader);
            }
        }
    }
}
