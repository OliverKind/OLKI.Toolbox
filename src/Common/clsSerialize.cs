/*
 * OLKI.Toolbox.Common
 * 
 * Initial Author: Oliver Kind - 2021
 * License:        LGPL
 * 
 * Desctiption:
 * Class that provides tool to serialize to a file and deserialize  objects from a file
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

using OLKI.Toolbox.src.Common;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OLKI.Toolbox.Common
{
    /// <summary>
    /// Class that provides tool to serialize to a file and deserialize  objects from a file
    /// </summary>
    public class Serialize
    {
        #region Constants
        /// <summary>
        /// Specifies the defaukt value if an errormessage should been show if an XML-Filce can not converted to an object
        /// </summary>
        private const bool DEFUALT_XML_TO_OBJECT_SHOW_ERROR_MESSAGE = true;
        #endregion

        #region Methodes
        public Serialize()
        {
        }

        #region ObjectToXMLString
        /// <summary>
        /// Serialize  an specified object to an string in XML scheme
        /// </summary>
        /// <param name="toSerialize">An Objext to serialize</param>
        public static string ObjectToXMLString(object toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
        #endregion

        #region ObjectToBase64String
        /// <summary>
        /// Serialize  an specified object to an BASE64 encoded string in XML scheme
        /// </summary>
        /// <param name="toSerialize">An Objext to serialize</param>
        /// <returns>An BASE64 encoded string in XML scheme contains an specified, serialize  object</returns>
        public static string ObjectToBase64String(object toSerialize)
        {
            // Works Only for Pagesettings
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, toSerialize);
                return Convert.ToBase64String(stream.ToArray());
            }
        }
        #endregion

        #region Base64StringToObject
        /// <summary>
        /// Deserialize  BASE64 encoded string in XML scheme to an object
        /// </summary>
        /// <param name="toDeserialize">Specifies the BASE64 encoded string in XML to deserialize</param>
        /// <returns>An object with data from the specified deserialized file or false if an exception occours</returns>
        public static object Base64StringToObject(string toDeserialize)
        {
            try
            {
                // Works Only for Pagesettings
                byte[] bytes = Convert.FromBase64String(toDeserialize);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    return new BinaryFormatter().Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(clsSerialize_StringTable._0x0001m, new object[] { ex.Message }), clsSerialize_StringTable._0x0001c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region ObjectToXML
        /// <summary>
        /// Serialize  an specified object to an specified file in XML scheme
        /// </summary>
        /// <param name="toSerialize">An Objext to serialize</param>
        /// <param name="ptah">A string that specifies the file path where the XML scheme should been wirtten to</param>
        public static bool ObjectToXML(object toSerialize, string ptah)
        {
            try
            {
                // Create an instance of the XmlSerializer specifying type and namespace.
                XmlSerializer serializer = new XmlSerializer(toSerialize.GetType());
                // Create an XmlTextWriter using a FileStream.
                Stream fs = new FileStream(ptah, FileMode.Create);
                XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, toSerialize);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(clsSerialize_StringTable._0x0002m, new object[] { toSerialize.ToString(), ptah, ex.Message }), clsSerialize_StringTable._0x0002c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region XMLToObject
        /// <summary>
        /// Deserialize  an specified XML file to an specified object and showing an error message if an exception occurs
        /// </summary>
        /// <param name="toDeserialize">The object to deserialize</param>
        /// <param name="ptah">A string that specifies the file path to the XML-File do deserialize</param>
        /// <returns>An object with data from the specified deserialized file or false if an exception occours</returns>
        public static object XMLToObject(object toDeserialize, string ptah)
        {
            return OLKI.Toolbox.Common.Serialize.XMLToObject(toDeserialize, ptah, DEFUALT_XML_TO_OBJECT_SHOW_ERROR_MESSAGE);
        }
        /// <summary>
        /// Deserialize  an specified XML file to an specified object
        /// </summary>
        /// <param name="toDeserialize">The object to deserialize</param>
        /// <param name="ptah">A string that specifies the file path to the XML-File do deserialize</param>
        /// <param name="showErrorMessage">Specifies if an error message should been shown if an exception during the deserialization occurs</param>
        /// <returns>An object with data from the specified deserialized file or false if an exception occours</returns>
        public static object XMLToObject(object toDeserialize, string ptah, bool showErrorMessage)
        {
            try
            {
                // Create an instance of the XmlSerializer specifying type and namespace.
                XmlSerializer serializer = new XmlSerializer(toDeserialize.GetType());

                // A FileStream is needed to read the XML document.
                FileStream fs = new FileStream(ptah, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);

                toDeserialize = serializer.Deserialize(reader);
                fs.Close();
                return toDeserialize;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                if (showErrorMessage)
                {
                    MessageBox.Show(string.Format(clsSerialize_StringTable._0x0003m, new object[] { ptah, ex.Message }), clsSerialize_StringTable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }
        #endregion

        #region GetFromXElement
        /// <summary>
        /// Return the value as boolean type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an boolean type</returns>
        public static bool GetFromXElement(XElement input, string elementName, bool valueIfNull)
        {

            try
            {
                return System.Convert.ToBoolean(input.Element(elementName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as double type of an specified element in an specified XElement or valueIfNull if the element was not found. CultureInfo("en-US")
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an double type</returns>
        public static decimal GetFromXElement(XElement input, string elementName, decimal valueIfNull)
        {

            try
            {
                return System.Convert.ToDecimal(input.Element(elementName).Value, new System.Globalization.CultureInfo("en-US"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as double type of an specified element in an specified XElement or valueIfNull if the element was not found. CultureInfo("en-US")
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an double type</returns>
        public static double GetFromXElement(XElement input, string elementName, double valueIfNull)
        {

            try
            {
                return System.Convert.ToDouble(input.Element(elementName).Value, new System.Globalization.CultureInfo("en-US"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as integer type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an integer type</returns>
        public static int GetFromXElement(XElement input, string elementName, int valueIfNull)
        {

            try
            {
                return System.Convert.ToInt32(input.Element(elementName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as long type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an long type</returns>
        public static long GetFromXElement(XElement input, string elementName, long valueIfNull)
        {

            try
            {
                return System.Convert.ToInt64(input.Element(elementName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as integer type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an integer type</returns>
        public static string GetFromXElement(XElement input, string elementName, string valueIfNull)
        {

            try
            {
                return System.Convert.ToString(input.Element(elementName).Value).Replace("\n", "\r\n");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as integer type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an integer type</returns>
        public static XElement GetFromXElement(XElement input, string elementName, XElement valueIfNull)
        {

            try
            {
                return input.Element(elementName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }
        #endregion

        #region GetFromXElementAttribute
        /// <summary>
        /// Return the value as boolean type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an boolean type</returns>
        public static bool GetFromXElementAttribute(XElement input, string elementName, string attributeName, bool valueIfNull)
        {

            try
            {
                return Convert.ToBoolean(input.Element(elementName).Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as boolean type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an boolean type</returns>
        public static bool GetFromXElementAttribute(XElement input, string attributeName, bool valueIfNull)
        {

            try
            {
                return Convert.ToBoolean(input.Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as double type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an double type</returns>
        public static double GetFromXElementAttribute(XElement input, string elementName, string attributeName, double valueIfNull)
        {

            try
            {
                return Convert.ToDouble(input.Element(elementName).Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as double type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an double type</returns>
        public static double GetFromXElementAttribute(XElement input, string attributeName, double valueIfNull)
        {

            try
            {
                return Convert.ToDouble(input.Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as integer type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an integer type</returns>
        public static int GetFromXElementAttribute(XElement input, string elementName, string attributeName, int valueIfNull)
        {

            try
            {
                return Convert.ToInt32(input.Element(elementName).Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as integer type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an integer type</returns>
        public static int GetFromXElementAttribute(XElement input, string attributeName, int valueIfNull)
        {

            try
            {
                return Convert.ToInt32(input.Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as long type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an long type</returns>
        public static long GetFromXElementAttribute(XElement input, string elementName, string attributeName, long valueIfNull)
        {

            try
            {
                return Convert.ToInt64(input.Element(elementName).Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as long type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an long type</returns>
        public static long GetFromXElementAttribute(XElement input, string attributeName, long valueIfNull)
        {

            try
            {
                return Convert.ToInt64(input.Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as integer type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="elementName">Specifies the Name of the Value to get from specified XElement</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an integer type</returns>
        public static string GetFromXElementAttribute(XElement input, string elementName, string attributeName, string valueIfNull)
        {

            try
            {
                return Convert.ToString(input.Element(elementName).Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }

        /// <summary>
        /// Return the value as integer type of an specified element in an specified XElement or valueIfNull if the element was not found
        /// </summary>
        /// <param name="input">Specifies the XElement to geht the value from</param>
        /// <param name="attributeName">Specifies the Name of the Attribute to get from specified XElement</param>
        /// <param name="valueIfNull">Specifies the value to return if the specified element can not found</param>
        /// <returns>The value of the specified element as an integer type</returns>
        public static string GetFromXElementAttribute(XElement input, string attributeName, string valueIfNull)
        {

            try
            {
                return Convert.ToString(input.Attribute(attributeName).Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return valueIfNull;
            }
        }
        #endregion
        #endregion
    }
}