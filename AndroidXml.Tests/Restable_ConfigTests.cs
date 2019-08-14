// Copyright (c) 2015 Quamotion
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using AndroidXml.Res;
using Xunit;

namespace AndroidXml.Tests
{
    public class Restable_ConfigTests
    {
        [Fact]
        public void GetLayoutDirectionTest()
        {
            // Arrange
            ResTable_config c = new ResTable_config();
            c.ScreenConfigScreenLayout = 0x80;

            // Act
            var value = c.ScreenConfigLayoutDirection;

            // Assert
            Assert.Equal(ConfigScreenLayoutDirection.LAYOUTDIR_RTL, value);
        }

        [Fact]
        public void SetLayoutDirectionTest()
        {
            // Arrange
            ResTable_config c = new ResTable_config();

            // Act
            c.ScreenConfigLayoutDirection = ConfigScreenLayoutDirection.LAYOUTDIR_RTL;

            // Assert
            Assert.Equal(0x80, c.ScreenConfigScreenLayout);
        }
    }
}
