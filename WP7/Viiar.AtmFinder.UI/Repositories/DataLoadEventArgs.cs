using System;

namespace Viiar.AtmFinder.UI.Repositories
{
    public class DataLoadEventArgs : EventArgs
    {
        public DataLoadEventArgs(string repository)
        {
            Repository = repository;
        }

        public string Repository { get; private set; }
    }
}
