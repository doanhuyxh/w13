using Framework;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class IplSessionGames : BaseService<SessionGames, int>, ISessionGames
    {
        public bool CreateSessionGame(int value1, int value2, ref int sessionId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Value1", value1);
                p.Add("@Value2", value2);
                p.Add("@Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var result = unitOfWork.ProcedureExecute("SessionGames_CreateSession", p);
                if (result)
                {
                    sessionId = p.Get<dynamic>("@Id");
                }
                return result;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }

        public SessionGames GetLastSession()
        {
            try
            {
                var data = unitOfWork.Procedure<SessionGames>("Session_GetLastSession").FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message,ex);
                return null;
            }
        }
        public SessionGames GetYetSession()
        {
            try
            {
                var data = unitOfWork.Procedure<SessionGames>("SessionGames_GetSessionCurrent").FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message,ex);
                return null;
            }
        }
    }
}
