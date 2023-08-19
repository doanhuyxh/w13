using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data.ES
{
    interface IESProvider<T>
    {
        IEnumerable<T> QueryString(string term);

        void AddUpdateEntity(T skillWithListOfDetails);
        void UpdateSkill(long updateId, string updateName, string updateDescription, List<T> updateSkillDetailsLis);
        void DeleteSkill(long updateId);
    }
}
