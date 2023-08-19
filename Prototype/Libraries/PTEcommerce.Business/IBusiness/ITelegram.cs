using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTEcommerce.Business.IBusiness
{
    public interface ITelegram
    {
        bool SendMessageToGroup(string message);
    }
}
