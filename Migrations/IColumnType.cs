using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations
{
    public interface IColumnType
    {
        string Int { get; }
        string String { get; }
        string StringFixedSize { get; }
        string Bool { get; }
        string Long { get; }
        string Short { get; }
        string Guid { get; }
        string DateTime { get; }
        string DateTime2 { get; }

    }

   
}
