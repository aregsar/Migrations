using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations.Schemas
{
    public class SqlSvr2012ColumnType : IColumnType
    {
        public string Int { get { return "int"; } }
        public string String { get { return "nvarchar"; } }
        public string StringFixedSize { get { return "nchar"; } }
        public string Bool { get { return "bit"; } }
        public string Long { get { return "bigint"; } }
        public string Short { get { return "smallint"; } }
        public string Guid { get { return "uniqueidentifier"; } }
        public string DateTime { get { return "datetime"; } }
        public string DateTime2 { get { return "datetime2(7)"; } }

    }

  
}
