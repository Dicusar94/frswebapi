using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MoackData
{
    public interface IDataBase
    {
        List<T>? SetContext<T>() where T : BaseEntity;
    }
}
