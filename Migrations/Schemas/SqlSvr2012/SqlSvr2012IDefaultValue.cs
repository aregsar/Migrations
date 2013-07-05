using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations.Schemas
{
    public class SqlSvr2012DefaultValue : IDefaultValue
    {


        public string NewGuid { get { return "newid()"; } }
        public string One { get { return "1"; } }
        public string Zero { get { return "0"; } }
        public string EmptyString { get { return "''"; } }
        public string MinusOne { get { return "-1"; } }
        public string GetDate { get { return "GetDate()"; } }
        public string GetUtcDate { get { return "GetUtcDate()"; } }
        public string GetDate2 { get { return "SysDateTime()"; } }
        public string GetUtcDate2 { get { return "SysUtcDateTime()"; } }

    }
}
