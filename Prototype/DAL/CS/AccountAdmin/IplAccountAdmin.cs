using Framework;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class IplAccountAdmin: BaseService<AccountAdmin, int>, IAccountAdmin
    {
        public AccountAdmin ViewDetailByUserNamePassword(string username, string password)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Username", username);
                p.Add("@Password", password);
                var data = unitOfWork.Procedure<AccountAdmin>("[Account_ViewDetailByUsernamePassword]", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public AccountAdmin ViewDetailByUsername(string username)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Username", username);
                var data = unitOfWork.Procedure<AccountAdmin>("Account_ViewDetailByUsername", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<AccountAdminPagging> ListAllPaging(int pageIndex, int pageSize)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@PageIndex", pageIndex);
                p.Add("@PageSize", pageSize);
                var data = unitOfWork.Procedure<AccountAdminPagging>("AccountAdmin_ListAllPaging", p).ToList();
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
