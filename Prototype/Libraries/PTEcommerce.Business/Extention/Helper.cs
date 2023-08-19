using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PTEcommerce.Business
{
    public class Helper
    {
        public static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }

            return Sb.ToString();
        }
        public static string md5_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (MD5 hash = MD5CryptoServiceProvider.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }

            return Sb.ToString();
        }
        public static bool ValidateEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public static bool ValidatePhone(string phone)
        {
            try
            {
                var phoneRegex = new Regex(@"(0[1-9]{1})+([0-9]{8})\b");
                var flag = phoneRegex.IsMatch(phone);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public static string MoneyFormat(decimal m)
        {
            return Math.Ceiling(m).ToString("N", new CultureInfo("vi-VN")).Replace(",00", "đ");
        }
        public static string NumberFormat(decimal m)
        {
            return Math.Ceiling(m).ToString("N", new CultureInfo("vi-VN")).Replace(",00", "");
        }
        private static bool CheckCodeSeriZing(string seri_card)
        {
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            if (!StringContains(seri_card.ToLower(), lowercase) || !StringContains(seri_card.ToLower(), digits))
            {
                return false;
            }
            return true;
        }
        private static bool CheckCodeSeriAllNetwork(string card_code, string seri_card)
        {
            if (!Regex.IsMatch(card_code, "^[0-9]*$"))
            {
                return false;
            }
            if (!Regex.IsMatch(seri_card, "^[0-9]*$"))
            {
                return false;
            }
            return true;
        }
        public static ConvertValueResult ConvertValue(string value)
        {
            ConvertValueResult result = new ConvertValueResult
            {
                value = new List<string>(),
                valuestring = string.Empty
            };
            if (value.Contains(","))
            {
                string[] arrValue = value.Split(',');
                var valueString = new List<string>();
                foreach(var item in arrValue) {
                    switch (item)
                    {
                        case "1":
                            valueString.Add("LỚN");
                            break;
                        case "2":
                            valueString.Add("NHỎ");
                            break;
                        case "3":
                            valueString.Add("ĐÔI");
                            break;
                        case "4":
                            valueString.Add("ĐƠN");
                            break;
                    }
                    result.value.Add(item);
                }
                result.valuestring = string.Join(" ", valueString);
            }
            else {
                switch (value)
                {
                    case "1":
                        result.valuestring = "LỚN";
                        break;
                    case "2":
                        result.valuestring = "NHỎ";
                        break;
                    case "3":
                        result.valuestring = "ĐÔI";
                        break;
                    case "4":
                        result.valuestring = "ĐƠN";
                        break;
                }
                result.value.Add(value);
            }
            return result;
        }
        
        private static bool CheckCodeOrSeriAllNetwork(string card_code_or_seri)
        {
            if (!Regex.IsMatch(card_code_or_seri, "^[0-9]*$"))
            {
                return false;
            }
            return true;
        }
        private static bool StringContains(string target, string list)
        {
            return target.IndexOfAny(list.ToCharArray()) != -1;
        }
        private static string TruncatePercents(string input)
        {
            string truncate1 = Regex.Replace(input, @"-", "");
            string truncate2 = Regex.Replace(truncate1, @" ", "");
            string truncate3 = Regex.Replace(truncate2, @",", "");
            return truncate3;
        }
        public static long GetTimeUnix()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        public static string GetToken(string vid)
        {
            return sha256_hash(vid + GetTimeUnix());
        }
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        public static string GenerateAgo(DateTime date)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "1 giây trước" : ts.Seconds + " giây trước";

            if (delta < 2 * MINUTE)
                return "1 phút trước";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút trước";

            if (delta < 90 * MINUTE)
                return "1 giờ trước";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 48 * HOUR)
                return "hôm qua";

            if (delta < 30 * DAY)
                return date.ToString("dd/MM/yyyy HH:mm:ss");

            if (delta < 12 * MONTH)
            {
                return date.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                return date.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
    }
}
