using System;

namespace Viiar.AtmFinder.Core
{
    public interface IResourceFileReader
    {
        string ReadToEnd(Uri uri);
    }
}
