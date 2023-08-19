using System.Globalization;


namespace PTEcommerce.Web.VNPAY
{
    public class VnPayCompare
    {
        public int Compare(string x, string y)
        {
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}