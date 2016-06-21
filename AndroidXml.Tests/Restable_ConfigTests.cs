using AndroidXml.Res;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidXml.Tests
{
    [TestClass]
    public class Restable_ConfigTests
    {
        [TestMethod]
        public void GetLayoutDirectionTest()
        {
            // Arrange
            ResTable_config c = new ResTable_config();
            c.ScreenConfigScreenLayout = 0x80;

            // Act
            var value = c.ScreenConfigLayoutDirection;

            // Assert
            Assert.AreEqual(ConfigScreenLayoutDirection.LAYOUTDIR_RTL, value);
        }

        [TestMethod]
        public void SetLayoutDirectionTest()
        {
            // Arrange
            ResTable_config c = new ResTable_config();

            // Act
            c.ScreenConfigLayoutDirection = ConfigScreenLayoutDirection.LAYOUTDIR_RTL;

            // Assert
            Assert.AreEqual(0x80, c.ScreenConfigScreenLayout);
        }
    }
}
