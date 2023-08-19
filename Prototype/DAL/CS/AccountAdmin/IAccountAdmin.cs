using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public interface IAccountAdmin : IBaseService<AccountAdmin, int>
    {
        AccountAdmin ViewDetailByUserNamePassword(string username, string password);
        AccountAdmin ViewDetailByUsername(string username);
        List<AccountAdminPagging> ListAllPaging(int pageIndex, int pageSize);
    }
}
