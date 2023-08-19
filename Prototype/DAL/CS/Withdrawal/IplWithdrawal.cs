using Framework;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class IplWithdrawal : BaseService<Withdrawal, int>, IWithdrawal
    {
        public List<WithdrawalExtend> ListAllPagingByCustomer(int idAccount, int pageIndex, int pageSize)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@PageIndex", pageIndex);
                p.Add("@PageSize", pageSize);
                var data = unitOfWork.Procedure<WithdrawalExtend>("[Withdrawal_ListAllPagingByCustomerByCondition]", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<WithdrawalExtend> ListAllPagingByCustomer(int idAccount, int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@PageIndex", pageIndex);
                p.Add("@PageSize", pageSize);
                p.Add("@TotalRow", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var data = unitOfWork.Procedure<WithdrawalExtend>("Withdrawal_ListAllPagingByCustomer", p).ToList();
                totalRow = p.Get<dynamic>("@TotalRow");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<WithdrawalExtend> ListAllPaging(int idAccount, int status, int pageIndex, int pageSize)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@Status", status);
                p.Add("@PageIndex", pageIndex);
                p.Add("@PageSize", pageSize);
                var data = unitOfWork.Procedure<WithdrawalExtend>("Withdrawal_ListAllPaging", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public Withdrawal CheckQuantityWithdrawal(int idAccount)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                var flag = unitOfWork.Procedure<Withdrawal>("Withdrawal_CheckTotalWithdrawal", p).FirstOrDefault();
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
    }
}
