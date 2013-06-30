using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations
{
    public interface INullabilityType
    {
        string Null{ get; }
        string NotNull { get; }
    }

  
}
