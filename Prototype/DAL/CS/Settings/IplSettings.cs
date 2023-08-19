using Framework;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class IplSettings : BaseService<Settings, int>, ISettings
    {
        public Settings GetValueByKey(string key)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Key", key);
                var data = unitOfWork.Procedure<Settings>("Setting_GetValueByKey", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public bool UpdateSetting(int id, string value)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                p.Add("@Value_Setting", value);
                var flag = unitOfWork.ProcedureExecute("Settings_UpdateSetting", p);
                return flag;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public List<Settings> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var data = unitOfWork.Procedure<Settings>("Settings_ListAllPaging", p).ToList();
                totalRow = p.Get<dynamic>("@totalRow");
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
