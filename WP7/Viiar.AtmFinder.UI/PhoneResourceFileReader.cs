using System;
using System.IO;
using System.Windows;
using Viiar.AtmFinder.Core;

namespace Viiar.AtmFinder.UI
{
    public class PhoneResourceFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            var stream = Application.GetResourceStream(uri);
            if (stream == null)
            {
                return null;
            }

            using (var reader = new StreamReader(stream.Stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
