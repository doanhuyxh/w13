using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public interface IHistoryTransfer: IBaseService<HistoryTransfer, int>
    {
        List<HistoryTransferExtend> ListAllPagingByCustomer(int idAccount, int pageIndex, int pageSize, ref int totalRow);
        List<HistoryTransferExtend> ListAllPaging(int idAccount, int type, int pageIndex, int pageSize);
        List<HistoryTransferExtend> ListAllPagingByCustomerCondition(int idAccount, int year, int month, int pageIndex, int pageSize);
    }
}
