using System;
using System.Linq;

namespace Viiar.AtmFinder.Core.Repositories
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class RepositoryAttribute : Attribute
    {
        public string ChainName { get; set; }
        public string Country { get; set; }

        public static RepositoryAttribute ExtractFromType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var attribute = type.GetCustomAttributes(typeof(RepositoryAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                throw new InvalidOperationException(
                        "Type '" + type.FullName + "' is not decorated with 'Repository' attribute");
            }

            return (RepositoryAttribute)attribute;
        }
    }
}
