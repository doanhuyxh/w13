using Framework;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class IplHistoryTransfer: BaseService<HistoryTransfer, int>, IHistoryTransfer
    {
        public List<HistoryTransferExtend> ListAllPagingByCustomer(int idAccount, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var data = unitOfWork.Procedure<HistoryTransferExtend>("HistoryTransfer_ListAllPagingByCustomer", p).ToList();
                totalRow = p.Get<dynamic>("@totalRow");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<HistoryTransferExtend> ListAllPagingByCustomerCondition(int idAccount, int year, int month, int pageIndex, int pageSize)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@Year", year);
                p.Add("@Month", month);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                var data = unitOfWork.Procedure<HistoryTransferExtend>("[HistoryTransfer_ListAllPagingByCustomerCondition]", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<HistoryTransferExtend> ListAllPaging(int idAccount, int type, int pageIndex, int pageSize)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@Type", type);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                var data = unitOfWork.Procedure<HistoryTransferExtend>("[HistoryTransfer_ListAllPaging]", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public List<HistoryTransfer> GetAllTranferByUser(int idAccount)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@AccountId", idAccount);
                var data = unitOfWork.Procedure<HistoryTransfer>("GetHistoryTransfersByAccountId", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

    }
}
