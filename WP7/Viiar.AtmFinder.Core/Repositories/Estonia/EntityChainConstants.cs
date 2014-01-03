using System;
using System.Collections.Generic;

namespace Viiar.AtmFinder.Core.Repositories.Estonia
{
    public static class EntityChainConstants
    {
        public const string SwedbankCode = "swedbank";
        public const string Swedbank = "Swedbank Eestis";
        public const string NordeaCode = "nordea";
        public const string Nordea = "Nordea Pank Eesti";
        public const string SebCode = "seb";
        public const string Seb = "Seb Eestis";
        public const string SampoCode = "sampo";
        public const string Sampo = "Sampo Panga";

        internal static readonly Dictionary<string, string> mapping = new Dictionary<string, string>
                                                                         {
                                                                                 { NordeaCode, Nordea }, 
                                                                                 { SebCode, Seb }, 
                                                                                 { SwedbankCode, Swedbank }, 
                                                                                 { SampoCode, Sampo }
                                                                         };

        public static string GetCode(string chainName)
        {
            if (string.IsNullOrEmpty(chainName))
            {
                throw new ArgumentNullException("chainName");
            }

            if (chainName.Equals(Swedbank, StringComparison.OrdinalIgnoreCase))
            {
                return SwedbankCode;
            }

            if (chainName.Equals(Nordea, StringComparison.OrdinalIgnoreCase))
            {
                return NordeaCode;
            }

            if (chainName.Equals(Seb, StringComparison.OrdinalIgnoreCase))
            {
                return SebCode;
            }

            if (chainName.Equals(Sampo, StringComparison.OrdinalIgnoreCase))
            {
                return SampoCode;
            }

            return chainName.ToLower();
        }

        public static string GetName(string chainCode)
        {
            if (chainCode == null)
            {
                throw new ArgumentNullException("chainCode");
            }

            return mapping[chainCode];
        }
    }
}
