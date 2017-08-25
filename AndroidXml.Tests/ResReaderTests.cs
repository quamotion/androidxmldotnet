// Copyright (c) 2016 Quamotion
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using AndroidXml.Res;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AndroidXml.Tests
{
    [TestClass]
    public class ResReaderTests
    {
        [TestMethod]
        [DeploymentItem("ApiDemos.resources.arsc")]
        public void ReadStringPoolTest()
        {
            using (Stream stream = File.OpenRead("ApiDemos.resources.arsc"))
            using (ResReader reader = new ResReader(stream))
            {
                // The first chunk must be of the type res_table_type; if not,
                // it is not an Android resources file.
                var header = reader.ReadResChunk_header();
                Assert.AreEqual(ResourceType.RES_TABLE_TYPE, header.Type);

                // From here, we can read the resTableHeader. It indicates the number
                // of packages that are contained in this file.
                var tableHeader = reader.ReadResTable_header(header);

                Assert.AreEqual(2u, tableHeader.PackageCount);

                header = reader.ReadResChunk_header();
                Assert.AreEqual(ResourceType.RES_STRING_POOL_TYPE, header.Type);

                var stringPoolHeader = reader.ReadResStringPool_header(header);
                var strings = reader.ReadResStringPool(stringPoolHeader);
            }
        }

        [TestMethod]
        [DeploymentItem("resources.arsc")]
        public void ReadStringPoolTest2()
        {
            using (Stream stream = File.OpenRead("resources.arsc"))
            using (ResReader reader = new ResReader(stream))
            {
                // The first chunk must be of the type res_table_type; if not,
                // it is not an Android resources file.
                var header = reader.ReadResChunk_header();
                Assert.AreEqual(ResourceType.RES_TABLE_TYPE, header.Type);

                // From here, we can read the resTableHeader. It indicates the number
                // of packages that are contained in this file.
                var tableHeader = reader.ReadResTable_header(header);

                Assert.AreEqual(1u, tableHeader.PackageCount);

                header = reader.ReadResChunk_header();
                Assert.AreEqual(ResourceType.RES_STRING_POOL_TYPE, header.Type);

                var stringPoolHeader = reader.ReadResStringPool_header(header);
                var strings = reader.ReadResStringPool(stringPoolHeader);

                var value = strings.StringData[0x109];
            }
        }
    }
}
