/*
 * OLKI.Toolbox.ColorAndPicture.Picture.Scan
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to scan single images
 * 
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the LGPL General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * LGPL General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not check the GitHub-Repository.
 * 
 * */

using OLKI.Toolbox.src.ColorAndPicture.Picture.Scan;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WIA;

namespace OLKI.Toolbox.ColorAndPicture.Picture.Scan
{
    /// <summary>
    /// Class that provides tool to scan single images
    /// </summary>
    public static class WIAscan
    {
        /// <summary>
        /// Default scan format
        /// </summary>
        const string SCAN_FORMAT_BMP = FormatID.wiaFormatJPEG;
        /// <summary>
        /// Adress to set horizontal scan resolution
        /// </summary>
        const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
        /// <summary>
        /// Adress to set vertical scan resolution
        /// </summary>
        const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";

        /// <summary>
        /// List all available scan devices
        /// </summary>
        /// <returns>List with available scan devices</returns>
        public static List<DeviceInfo> GetDevices()
        {
            List<DeviceInfo> DeviceList = new List<DeviceInfo>();
            DeviceManager DeviceManager = new DeviceManager();
            for (int i = 1; i <= DeviceManager.DeviceInfos.Count; i++)
            {
                if (DeviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                {
                    DeviceList.Add(DeviceManager.DeviceInfos[i]);
                }
            }
            return DeviceList;
        }

        /// <summary>
        /// Get the ExceptionMessage for the WIA-Exception an return a new exception with the WIA-Exception as inner exception
        /// </summary>
        /// <param name="wiaException"></param>
        /// <returns>Exception with ExceptionMessage for the WIA-exception, WIA-exception as inner exception </returns>
        private static Exception GetExceptionMessage(Exception wiaException)
        {
            switch ((uint)wiaException.HResult)
            {
                case 0x80210001:
                    return new Exception(StringTable._0x80210001, wiaException);
                case 0x80210002:
                    return new Exception(StringTable._0x80210002, wiaException);
                case 0x80210003:
                    return new Exception(StringTable._0x80210003, wiaException);
                case 0x80210004:
                    return new Exception(StringTable._0x80210004, wiaException);
                case 0x80210005:
                    return new Exception(StringTable._0x80210005, wiaException);
                case 0x80210006:
                    return new Exception(StringTable._0x80210006, wiaException);
                case 0x80210007:
                    return new Exception(StringTable._0x80210007, wiaException);
                case 0x80210008:
                    return new Exception(StringTable._0x80210008, wiaException);
                case 0x80210009:
                    return new Exception(StringTable._0x80210009, wiaException);
                case 0x8021000A:
                    return new Exception(StringTable._0x8021000A, wiaException);
                case 0x8021000B:
                    return new Exception(StringTable._0x8021000B, wiaException);
                case 0x8021000C:
                    return new Exception(StringTable._0x8021000C, wiaException);
                case 0x8021000D:
                    return new Exception(StringTable._0x8021000D, wiaException);
                case 0x8021000E:
                    return new Exception(StringTable._0x8021000E, wiaException);
                case 0x8021000F:
                    return new Exception(StringTable._0x8021000F, wiaException);
                case 0x80210015:
                    return new Exception(StringTable._0x80210015, wiaException);
                case 0x80210016:
                    return new Exception(StringTable._0x80210016, wiaException);
                case 0x80210017:
                    return new Exception(StringTable._0x80210017, wiaException);
                case 0x80210020:
                    return new Exception(StringTable._0x80210020, wiaException);
                case 0x80210021:
                    return new Exception(StringTable._0x80210021, wiaException);
                case 0x80210064:
                    return new Exception(StringTable._0x80210064, wiaException);
                default:
                    return new Exception(StringTable._0x80210001, wiaException);
            }
        }

        /// <summary>
        /// Scan an single image, with default resolution
        /// </summary>
        /// <param name="deviceId">Scan device</param>
        /// <returns>Scanned image or NULL if an exception was thrown</returns>
        public static Image Scan(string deviceId)
        {
            return Scan(deviceId, 0);
        }
        /// <summary>
        /// Scan an single image, with defined resolution
        /// </summary>
        /// <param name="deviceId">Scan device</param>
        /// <param name="resolution">Scan resolution</param>
        /// <returns>Scanned image or NULL if an exception was thrown</returns>
        public static Image Scan(string deviceId, uint resolution)
        {
            return Scan(deviceId, resolution, out _);
        }
        /// <summary>
        /// Scan an single image, with default resolution
        /// </summary>
        /// <param name="deviceId">Scan device</param>
        /// <param name="exception">Exception while scanning</param>
        /// <returns>Scanned image or NULL if an exception was thrown</returns>
        public static Image Scan(string deviceId, out Exception exception)
        {
            return Scan(deviceId, 0, out exception);
        }
        /// <summary>
        /// Scan an single image, with defined resolution
        /// </summary>
        /// <param name="deviceId">Scan device</param>
        /// <param name="resolution">Scan resolution</param>
        /// <param name="exception">Exception while scanning</param>
        /// <returns>Scanned image or NULL if an exception was thrown</returns>
        public static Image Scan(string deviceId, uint resolution, out Exception exception)
        {
            exception = null;

            // Connect do scanner
            Device Device = Connect(deviceId);
            if (Device == null) return null;

            // Scan first page
            try
            {
                Item DeviceItem = Device.Items[1];

                if (resolution > 0)
                {
                    SetProperty(DeviceItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, resolution);
                    SetProperty(DeviceItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, resolution);
                }

                CommonDialog ScanDialog = new WIA.CommonDialog();
                ImageFile ScanImage = (ImageFile)ScanDialog.ShowTransfer(DeviceItem, SCAN_FORMAT_BMP, true);

                return Image.FromStream(new MemoryStream((byte[])ScanImage.FileData.get_BinaryData()));
            }
            catch (Exception ex)
            {
                exception = GetExceptionMessage(ex);
            }
            return null;
        }

        /// <summary>
        /// Set an scan device property
        /// </summary>
        /// <param name="properties">Set of properties</param>
        /// <param name="propertyId">Id of the property to set</param>
        /// <param name="value">Value to set the property to</param>
        private static void SetProperty(Properties properties, object propertyId, object value)
        {
            properties.get_Item(ref propertyId).set_Value(ref value);
        }

        /// <summary>
        /// Connect to an defined scan device and return the DeviceInfo or NULL if the connection failed
        /// </summary>
        /// <param name="deviceId">Id of the device to connect to</param>
        /// <returns>DeviceInfo of the connected scan device or NULL if the connection failed</returns>
        private static Device Connect(string deviceId)
        {
            foreach (DeviceInfo DeviceInfo in new DeviceManager().DeviceInfos)
            {
                if (DeviceInfo.DeviceID == deviceId) return DeviceInfo.Connect();
            }
            return null;
        }
    }
}