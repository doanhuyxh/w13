using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PTEcommerce.Business
{
    public class Common
    {
        public static string FormatCurrence (decimal amount)
        {
            return string.Format("{0:N0}", amount) + " đ";
        }
        /// <summary>
        /// Get First Day Of Month
        /// </summary>
        /// <param name="iMonth">Month of year</param>
        /// <param name="year">year</param>
        /// <returns>FirstDayOfMonth</returns>
        public static DateTime GetFirstDayOfMonth(int iMonth, int year=0)
        {
            if (year == 0)
                year = DateTime.Now.Year;

            DateTime dtResult = new DateTime(year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Get Last Day Of Month
        /// </summary>
        /// <param name="iMonth">Month of year</param>
        /// <param name="year">year</param>
        /// <returns>LastDayOfMonth</returns>
        public static DateTime GetLastDayOfMonth(int iMonth, int year = 0)
        {
            if (year == 0)
                year = DateTime.Now.Year;

            DateTime dtResult = new DateTime(year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        #region DateTime handler
        public static string AddDayByDateString(string date)
        {
            var returnDate = string.Empty;

            return returnDate;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }/// <summary>
         /// 
         /// </summary>
         /// <param name="iMonth"></param>
         /// <returns></returns>
        public static DateTime GetLastDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        #endregion

        #region Convert String To...
        public static int StringToInt(string num, int defVal = 0)
        {
            try
            {
                return int.Parse(num);
            }
            catch
            {
                return defVal;
            }
        }

        public static bool StringToBool(string num, bool defVal = false)
        {
            try
            {
                if (num == "1")
                    return true;
                return bool.Parse(num);
            }
            catch
            {
                return defVal;
            }
        }
        public static long StringToLong(string num, long defVal = 0)
        {
            try
            {
                return long.Parse(num);
            }
            catch
            {
                return defVal;
            }
        }
        #endregion

        #region BaseCode
        /// <summary>
        /// Base64Encode function
        /// </summary>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string Base64Encode(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        /// <summary>
        /// Base64Decode
        /// </summary>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string Base64Decode(string sData)
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
        #endregion
    }
    public static class ExtendedMethods
    {
        #region string
        public static int? ToInt(this string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DateTime? ConvertToDate(this string value, string format = "MM/dd/yyyy")
        {
            try
            {
                DateTime _return;
                double tempValue1;
                if (DateTime.TryParse(value, out _return))
                {

                }
                else if (double.TryParse(value, out tempValue1))
                {
                    _return = DateTime.FromOADate(tempValue1);
                }
                else
                {
                    _return = DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
                }
                return _return;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}