using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public interface IPlayGames : IBaseService<PlayGames, int>
    {
        List<PlayGames> GetListPlayGameBySessionId(long sessionId);
        List<PlayGamesHistory> GetListPlayGameByMe(int idAccount, int offset, int limit);
        List<PlayGames> GetListPlayGameBySessionIdAndIdAccount(long sessionId, int idAccount);
        List<PlayGamesHistory> GetListPlayGameAllBySessionId(long sessionId);
        List<PlayGamesHistory> GetListHistoryPlayGame(int idAccount, int pageIndex, int pageSize);
        List<PlayGamesHistory> GetTop10();
    }
}
