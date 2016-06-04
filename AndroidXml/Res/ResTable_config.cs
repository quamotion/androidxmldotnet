// Copyright (c) 2012 Markus Jarderot
// Copyright (c) 2015 Quamotion
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System;
using AndroidXml.Utils;

namespace AndroidXml.Res
{
#if !NETSTANDARD1_5
    [Serializable]
#endif
    public class ResTable_config
    {
        // Original properties
        public uint Size { get; set; }
        public uint IMSI { get; set; }
        public uint Locale { get; set; }
        public uint ScreenType { get; set; }
        public uint Input { get; set; }
        public uint ScreenSize { get; set; }
        public uint Version { get; set; }
        public uint ScreenConfig { get; set; }
        public uint ScreenSizeDp { get; set; }

        #region Derived properties

        #region IMSI derived properties

        /// Mobile country code (from SIM). 0 means "any"
        public ushort IMSI_MCC
        {
            get { return (ushort) Helper.GetBits(IMSI, 0xFFFFu, 16); }
            set { IMSI = Helper.SetBits(IMSI, value, 0xFFFFu, 16); }
        }

        /// Mobile network code (from SIM). 0 means "any"
        public ushort IMSI_MNC
        {
            get { return (ushort) Helper.GetBits(IMSI, 0xFFFFu, 0); }
            set { IMSI = Helper.SetBits(IMSI, value, 0xFFFFu, 0); }
        }

        #endregion

        #region Locale derived properties

        public string LocaleLanguage
        {
            get
            {
                byte[] bytes = BitConverter.GetBytes(Locale);
                return new string(new[] {(char) bytes[0], (char) bytes[1]});
            }
            set
            {
                if (value.Length != 2) throw new ArgumentException();
                byte[] bytes = BitConverter.GetBytes(Locale);
                bytes[0] = (byte) value[0];
                bytes[1] = (byte) value[1];
                Locale = BitConverter.ToUInt32(bytes, 0);
            }
        }

        public string LocaleCountry
        {
            get
            {
                byte[] bytes = BitConverter.GetBytes(Locale);
                return new string(new[] {(char) bytes[2], (char) bytes[3]});
            }
            set
            {
                if (value.Length != 2) throw new ArgumentException();
                byte[] bytes = BitConverter.GetBytes(Locale);
                bytes[2] = (byte) value[0];
                bytes[3] = (byte) value[1];
                Locale = BitConverter.ToUInt32(bytes, 0);
            }
        }

        #endregion

        #region ScreenType derived properties

        public ConfigOrientation ScreenTypeOrientation
        {
            get { return (ConfigOrientation) Helper.GetBits(ScreenType, 0xFFu, 0); }
            set { ScreenType = Helper.SetBits(ScreenType, (uint) value, 0xFFu, 0); }
        }

        public ConfigTouchscreen ScreenTypeTouchscreen
        {
            get { return (ConfigTouchscreen) Helper.GetBits(ScreenType, 0xFFu, 8); }
            set { ScreenType = Helper.SetBits(ScreenType, (uint) value, 0xFFu, 8); }
        }

        public ConfigDensity ScreenTypeDensity
        {
            get { return (ConfigDensity) Helper.GetBits(ScreenType, 0xFFFFu, 16); }
            set { ScreenType = Helper.SetBits(ScreenType, (uint) value, 0xFFFFu, 16); }
        }

        #endregion

        #region Input derived properties

        public ConfigKeyboard InputKeyboard
        {
            get { return (ConfigKeyboard) Helper.GetBits(Input, 0xFF, 24); }
            set { Input = Helper.SetBits(Input, (uint) value, 0xFF, 24); }
        }

        public ConfigNavigation InputNavigation
        {
            get { return (ConfigNavigation) Helper.GetBits(Input, 0xFF, 16); }
            set { Input = Helper.SetBits(Input, (uint) value, 0xFF, 16); }
        }

        public ConfigKeysHidden InputKeysHidden
        {
            get { return (ConfigKeysHidden) Helper.GetBits(Input, 0x3u, 8); }
            set { Input = Helper.SetBits(Input, (uint) value, 0x3u, 8); }
        }

        public ConfigNavHidden InputNavHidden
        {
            get { return (ConfigNavHidden) Helper.GetBits(Input, 0x3u, 10); }
            set { Input = Helper.SetBits(Input, (uint) value, 0x3u, 10); }
        }

        #endregion

        #region ScreenSize derived properties

        public ushort ScreenSizeWidth
        {
            get { return (ushort) Helper.GetBits(ScreenSize, 0xFFFFu, 16); }
            set { ScreenSize = Helper.SetBits(ScreenSize, value, 0xFFFFu, 16); }
        }

        public ushort ScreenSizeHeight
        {
            get { return (ushort) Helper.GetBits(ScreenSize, 0xFFFFu, 0); }
            set { ScreenSize = Helper.SetBits(ScreenSize, value, 0xFFFFu, 0); }
        }

        #endregion

        #region Version derived properties

        public ushort VersionSDK
        {
            get { return (ushort) Helper.GetBits(Version, 0xFFFFu, 16); }
            set { Version = Helper.SetBits(Version, value, 0xFFFFu, 16); }
        }

        public ushort VersionMinor
        {
            get { return (ushort) Helper.GetBits(Version, 0xFFFFu, 0); }
            set { Version = Helper.SetBits(Version, value, 0xFFFFu, 0); }
        }

        #endregion

        #region ScreenConfig derived properties
        public ConfigScreenLayoutDirection ScreenConfigLayoutDirection
        {
            get { return (ConfigScreenLayoutDirection)Helper.GetBits(ScreenConfig, 0x3f, 6); }
            set { ScreenConfig = Helper.SetBits(ScreenConfig, (uint)value, 0x3f, 6); }
        }

        public ConfigScreenSize ScreenConfigScreenSize
        {
            get { return (ConfigScreenSize) Helper.GetBits(ScreenConfig, 0x0f, 0); }
            set { ScreenConfig = Helper.SetBits(ScreenConfig, (uint) value, 0x0f, 0); }
        }

        public ConfigScreenLong ScreenConfigScreenLong
        {
            get { return (ConfigScreenLong) Helper.GetBits(ScreenConfig, 0x30, 4); }
            set { ScreenConfig = Helper.SetBits(ScreenConfig, (uint) value, 0x30, 4); }
        }

        public ConfigUIModeType ScreenConfigUIModeType
        {
            get { return (ConfigUIModeType) Helper.GetBits(ScreenConfig, 0xFu, 16); }
            set { ScreenConfig = Helper.SetBits(ScreenConfig, (uint) value, 0xFu, 16); }
        }

        public ConfigUIModeNight ScreenConfigUIModeNight
        {
            get { return (ConfigUIModeNight) Helper.GetBits(ScreenConfig, 0x3u, 20); }
            set { ScreenConfig = Helper.SetBits(ScreenConfig, (uint) value, 0x3u, 20); }
        }

        public ushort ScreenConfigSmallestScreenWidthDp
        {
            get { return (ushort) Helper.GetBits(ScreenConfig, 0xFFFF0000u, 16); }
            set { ScreenConfig = Helper.SetBits(ScreenConfig, value, 0xFFFF0000u, 16); }
        }

        #endregion

        #region ScreenSizeDp derived properties

        public ushort ScreenSizeDpWidth
        {
            get { return (ushort)Helper.GetBits(ScreenSizeDp, 0xFFFFu, 0); }
            set { ScreenSizeDp = Helper.SetBits(ScreenSizeDp, value, 0xFFFFu, 0); }
        }

        public ushort ScreenSizeDpHeight
        {
            get { return (ushort)Helper.GetBits(ScreenSizeDp, 0xFFFFu, 16); }
            set { ScreenSizeDp = Helper.SetBits(ScreenSizeDp, value, 0xFFFFu, 16); }
        }

        #endregion

        #endregion
    }

    #region Enums

    public enum ConfigOrientation
    {
        ORIENTATION_ANY = 0x0000,
        ORIENTATION_PORT = 0x0001,
        ORIENTATION_LAND = 0x0002,
        ORIENTATION_SQUARE = 0x0003,
    }

    public enum ConfigTouchscreen
    {
        TOUCHSCREEN_ANY = 0x0000,
        TOUCHSCREEN_NOTOUCH = 0x0001,
        TOUCHSCREEN_STYLUS = 0x0002,
        TOUCHSCREEN_FINGER = 0x0003,
    }

    public enum ConfigDensity
    {
        DENSITY_DEFAULT = 0,
        DENSITY_LOW = 120,
        DENSITY_MEDIUM = 160,
        DENSITY_TV = 213,
        DENSITY_HIGH = 240,
        DENSITY_XHIGH = 320,
        DENSITY_XXHIGH = 480,
        DENSITY_XXXHIGH = 640,
        DENSITY_NONE = 0xffff,
    }

    public enum ConfigKeyboard
    {
        KEYBOARD_ANY = 0x0000,
        KEYBOARD_NOKEYS = 0x0001,
        KEYBOARD_QWERTY = 0x0002,
        KEYBOARD_12KEY = 0x0003,
    }

    public enum ConfigNavigation
    {
        NAVIGATION_ANY = 0x0000,
        NAVIGATION_NONAV = 0x0001,
        NAVIGATION_DPAD = 0x0002,
        NAVIGATION_TRACKBALL = 0x0003,
        NAVIGATION_WHEEL = 0x0004,
    }

    public enum ConfigKeysHidden
    {
        KEYSHIDDEN_ANY = 0x0000,
        KEYSHIDDEN_NO = 0x0001,
        KEYSHIDDEN_YES = 0x0002,
        KEYSHIDDEN_SOFT = 0x0003,
    }

    public enum ConfigNavHidden
    {
        NAVHIDDEN_ANY = 0x0000,
        NAVHIDDEN_NO = 0x0001,
        NAVHIDDEN_YES = 0x0002,
    }

    public enum ConfigScreenLayoutDirection
    {
        LAYOUTDIR_ANY  = 0x00,
        LAYOUTDIR_LTR = 0x01,
        LAYOUTDIR_RTL = 0x02
    }

    public enum ConfigScreenSize
    {
        SCREENSIZE_ANY = 0x00,
        SCREENSIZE_SMALL = 0x01,
        SCREENSIZE_NORMAL = 0x02,
        SCREENSIZE_LARGE = 0x03,
        SCREENSIZE_XLARGE = 0x04,
    }

    public enum ConfigScreenLong
    {
        SCREENLONG_ANY = 0x00,
        SCREENLONG_NO = 0x01,
        SCREENLONG_YES = 0x02,
    }

    public enum ConfigUIModeType
    {
        UI_MODE_TYPE_ANY = 0x00,
        UI_MODE_TYPE_NORMAL = 0x01,
        UI_MODE_TYPE_DESK = 0x02,
        UI_MODE_TYPE_CAR = 0x03,
        UI_MODE_TYPE_TELEVISION = 0x04,
    }

    public enum ConfigUIModeNight
    {
        UI_MODE_NIGHT_ANY = 0x00,
        UI_MODE_NIGHT_NO = 0x01,
        UI_MODE_NIGHT_YES = 0x02,
    }

    #endregion
}