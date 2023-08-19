using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public interface IWithdrawal :IBaseService<Withdrawal, int>
    {
        List<WithdrawalExtend> ListAllPaging(int idAccount, int status, int pageIndex, int pageSize);
        List<WithdrawalExtend> ListAllPagingByCustomer(int idAccount, int pageIndex, int pageSize);
        List<WithdrawalExtend> ListAllPagingByCustomer(int idAccount, int pageIndex, int pageSize, ref int totalRow);
        Withdrawal CheckQuantityWithdrawal(int idAccount);
    }
}
