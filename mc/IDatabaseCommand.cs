using System;

namespace mc
{
    public interface IDatabaseCommand
    {
        void Process(string[] args);
    }
}
