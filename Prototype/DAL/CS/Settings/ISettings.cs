using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    public interface ISettings: IBaseService<Settings, int>
    {
        Settings GetValueByKey(string key);
        bool UpdateSetting(int id, string value);
        List<Settings> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);
    }
}
