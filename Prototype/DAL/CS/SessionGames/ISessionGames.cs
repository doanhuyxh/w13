using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public interface ISessionGames : IBaseService<SessionGames, int>
    {
        bool CreateSessionGame(int value1, int value2, ref int sessionId);
        SessionGames GetLastSession();
        SessionGames GetYetSession();
    }
}
