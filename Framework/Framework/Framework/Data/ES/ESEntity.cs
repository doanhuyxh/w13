using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data.ES
{
    public abstract class ESEntity:IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
