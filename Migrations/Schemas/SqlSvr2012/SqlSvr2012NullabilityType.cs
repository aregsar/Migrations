using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations.Schemas
{
    public class SqlSvr2012NullabilityType : INullabilityType
    {


        public string Null { get { return "null"; } }
        public string NotNull { get { return "not null"; } }

    }
}
