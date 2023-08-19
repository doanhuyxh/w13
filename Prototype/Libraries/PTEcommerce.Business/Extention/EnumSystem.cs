using System.Collections.Generic;
using System.Web.Mvc;

namespace PTEcommerce.Business
{
    public class EnumSystem
    {
        public enum EnumStatusOrderCardBuy
        {
            create = 1,
            done = 2,
            donebutfail = 3,
            cancel = 4
        }
        public enum EnumTypeHistoryTransfer
        {
            rechargeamount = 1,
            buyservice = 2,
            transfermoney = 3,
            withdrawal = 4,
            addamount = 5,
            rechargecard = 6,
            bonus = 7
        }

    }
}
