using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations
{

    public interface IDefaultValue
    {
        string NewGuid { get; }   
        string One { get; }
        string Zero { get; }
        string EmptyString { get; }
        string MinusOne { get; }
        string GetDate { get; }
        string GetUtcDate { get; }
        string GetDate2 { get; }
        string GetUtcDate2 { get; }
    }
 
}
