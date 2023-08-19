using System.Configuration;

namespace PTEcommerce.Business
{
    public class ConstKey
    {
        public static string keySHA = ConfigurationManager.AppSettings["keySHA"] ?? "hubtransfer";
        public static string Domain = ConfigurationManager.AppSettings["Domain"] ?? "http://cardvip.vn/";
        public static int TypeCardBuy = 1;
        public static int TypeCardSell = 2;
    }
}
