using Framework;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public class IplPlayGames : BaseService<PlayGames, int>, IPlayGames
    {
        public List<PlayGamesHistory> GetListPlayGameByMe(int idAccount, int offset, int limit)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@PageIndex", offset);
                p.Add("@PageSize", limit);
                var data = unitOfWork.Procedure<PlayGamesHistory>("PlayGames_GetHistoryByMe", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public List<PlayGames> GetListPlayGameBySessionId(long sessionId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@SessionId", sessionId);
                var data = unitOfWork.Procedure<PlayGames>("PlayGames_GetAllSessionById", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<PlayGamesHistory> GetListPlayGameAllBySessionId(long sessionId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@SessionId", sessionId);
                var data = unitOfWork.Procedure<PlayGamesHistory>("PlayGames_GetAllSessionById", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<PlayGames> GetListPlayGameBySessionIdAndIdAccount(long sessionId, int idAccount)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@SessionId", sessionId);
                p.Add("@IdAccount", idAccount);
                var data = unitOfWork.Procedure<PlayGames>("PlayGames_CheckExists", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<PlayGamesHistory> GetListHistoryPlayGame(int idAccount, int pageIndex, int pageSize)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@IdAccount", idAccount);
                p.Add("@PageIndex", pageIndex);
                p.Add("@PageSize", pageSize);
                var data = unitOfWork.Procedure<PlayGamesHistory>("PlayGames_GetListHistory", p).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }
        public List<PlayGamesHistory> GetTop10()
        {
            try
            {
                var data = unitOfWork.Procedure<PlayGamesHistory>("PlayGames_GetTop10").ToList();
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
