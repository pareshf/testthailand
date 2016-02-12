using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using CRM.Model.General;
using System.Net.Mail;

namespace CRM.Core.Utility
{
    public static class Utility
    {
        /// <summary>
        /// Converts YearMonth numner format value into YearMonth string format.
        /// </summary>
        /// <param name="yearMonthNumber">Accept Year month in number format.</param>
        /// <returns>Year month string value. </returns>
        public static String ConvertYearMonthNumberToYearMonthString(Int32 yearMonthNumber)
        {
            return yearMonthNumber.ToString();
        }

        /// <summary>
        /// Converts YearMonth string format value into YearMonth number format.
        /// </summary>
        /// <param name="yearMonthString">Accept Year month in full string format.</param>
        /// <returns>Year month number value. </returns>
        public static Int32 ConvertYearMonthStringToYearMonthNumber(String yearMonthString)
        {
            String monthName = String.Empty;
            String yearNumber = String.Empty;
            String yearMonthNumber = String.Empty;
            int len = yearMonthString.Length;
            yearNumber = yearMonthString.Substring(len - 4, 4);
            monthName = yearMonthString.Replace(yearNumber, "");

            switch (monthName.Trim().ToLower())
            {
                case "january":
                    yearMonthNumber = yearNumber + "01";
                    break;
                case "february":
                    yearMonthNumber = yearNumber + "02";
                    break;
                case "march":
                    yearMonthNumber = yearNumber + "03";
                    break;
                case "april":
                    yearMonthNumber = yearNumber + "04";
                    break;
                case "may":
                    yearMonthNumber = yearNumber + "05";
                    break;
                case "june":
                    yearMonthNumber = yearNumber + "06";
                    break;
                case "july":
                    yearMonthNumber = yearNumber + "07";
                    break;
                case "august":
                    yearMonthNumber = yearNumber + "08";
                    break;
                case "september":
                    yearMonthNumber = yearNumber + "09";
                    break;
                case "october":
                    yearMonthNumber = yearNumber + "10";
                    break;
                case "november":
                    yearMonthNumber = yearNumber + "11";
                    break;
                case "december":
                    yearMonthNumber = yearNumber + "12";
                    break;
                default:
                    yearMonthNumber = "0";
                    break;
            }
            return Convert.ToInt32(yearMonthNumber);
        }

        /// <summary>
        /// Converts .xslt file into Htmal formate.
        /// </summary>
        /// <param name="xslTemplatePath"></param>
        /// <param name="xslTemplateName"></param>
        /// <param name="ObjDictionary"></param>
        /// <returns></returns>
        public static string TransformXsltToXhtml(string xsltTemplatePath, string xsltTemplateName, IDictionary objDictionary)
        {
            //XslCompiledTransform ObjXSLT = new XslCompiledTransform(); .Net Framework 2.0
            XslTransform ObjXSLT = new XslTransform();
            ObjXSLT.Load(xsltTemplatePath + xsltTemplateName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateElement("DocumentRoot"));
            XPathNavigator xpathNav = xmlDoc.CreateNavigator();

            XsltArgumentList xslArg = new XsltArgumentList();

            if (objDictionary != null)
            {
                foreach (DictionaryEntry entry in objDictionary)
                {
                    xslArg.AddExtensionObject(entry.Key.ToString(), entry.Value);
                }
            }

            StringBuilder emailbuilder = new StringBuilder();
            XmlTextWriter xmlwriter = new XmlTextWriter(new System.IO.StringWriter(emailbuilder));

            ObjXSLT.Transform(xpathNav, xslArg, xmlwriter, null);

            string bodytext;

            XmlDocument xemaildoc = new XmlDocument();

            xemaildoc.LoadXml(emailbuilder.ToString());

            XmlNode bodynode = xemaildoc.SelectSingleNode("//body");

            bodytext = bodynode.InnerXml;
            if (bodytext.Length > 0)
            {
                bodytext = bodytext.Replace("&amp;", "&");
            }

            return bodytext;
        }

        /// <summary>
        /// Randomely generate alpha-numeric password.
        /// </summary>
        /// <param name="passwordLength">Length of password.</param>
        /// <returns>Return password.</returns>
        public static String GetRandomPassword(int passwordLength)
        {
            // Get the GUID
            string guidResult = System.Guid.NewGuid().ToString();

            // Remove the hyphens
            guidResult = guidResult.Replace("-", string.Empty);

            // Make sure length is valid
            if (passwordLength <= 0 || passwordLength > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

            // Return the first length bytes
            return guidResult.Substring(0, passwordLength);
        }

        public static bool IsInteger(string s)
        {
            Int32 n;
            return Int32.TryParse(s.Trim(), out n);
        }

        public static bool IsDecimal(string s)
        {
            Decimal n;
            return Decimal.TryParse(s.Trim(), out n);
        }

        public static bool IsDouble(string s)
        {
            Double n;
            return Double.TryParse(s.Trim(), out n);
        }

        public static bool IsFloat(string s)
        {
            float n;
            return float.TryParse(s.Trim(), out n);
        }

        public static bool IsDateTime(string s)
        {
            DateTime n;
            return DateTime.TryParse(s.Trim(), out n);
        }

        public static bool ConvertToBoolean(string s)
        {
            bool result = false;
            switch (s.ToLower())
            {
                case "true":
                    result = true;
                    break;
                case "yes":
                    result = true;
                    break;
                case "false":
                    result = false;
                    break;
                case "no":
                    result = false;
                    break;
                default:
                    result = false;
                    break;
            }



            return result;
        }
    }
}