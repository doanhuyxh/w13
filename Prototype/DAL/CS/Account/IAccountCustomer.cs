using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public interface IAccountCustomer : IBaseService<AccountCustomer, int>
    {
        AccountCustomer ViewDetailByUserNamePassword(string username, string password);
        List<AccountCustomerExtend> ListAllPaging(string searchString, int idAccount, int pageIndex, int pageSize);
        AccountCustomer ViewDetailByEmail(string email);
        AccountCustomer ViewDetailByUsername(string username);
        bool UpdateToken(int id, string token);
    }
}
