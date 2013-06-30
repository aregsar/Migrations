using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrations.Schemas;

namespace Migrations
{
    public class DatabaseAdapterFactory
    {
        public static IDatabaseAdapter GetAdater(string serverType)
        {
            return new SqlServer2012DatabaseAdapter();
        }
    }
}
