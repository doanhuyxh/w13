using Framework;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class IplAccountCustomer : BaseService<AccountCustomer, int>, IAccountCustomer
    {
        public AccountCustomer ViewDetailByUserNamePassword(string username, string password)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Username", username);
                p.Add("@Password", password);
                var data = unitOfWork.Procedure<AccountCustomer>("[AccountCustomer_ViewDetailByUserPass]", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public AccountCustomer ViewDetailByUsername(string username)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Username", username);
                var data = unitOfWork.Procedure<AccountCustomer>("[AccountCustomer_ViewDetailByUsername]", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public AccountCustomer ViewDetailByEmail(string email)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Email", email);
                var data = unitOfWork.Procedure<AccountCustomer>("[AccountCustomer_ViewDetailByEmail]", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<AccountCustomerExtend> ListAllPaging(string searchString, int idAccount, int pageIndex, int pageSize)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@SearchString", searchString);
                p.Add("@PageIndex", pageIndex);
                p.Add("@PageSize", pageSize);
                var data = unitOfWork.Procedure<AccountCustomerExtend>("AccountCustomer_ListAllPaging", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message,ex);
                return null;
            }
        }
        public bool UpdateToken(int id, string token)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                p.Add("@Token", token);
                var flag = unitOfWork.ProcedureExecute("AccountCustomer_UpdateToken", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
    }
}
