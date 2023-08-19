using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data.ES
{
    public class IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
