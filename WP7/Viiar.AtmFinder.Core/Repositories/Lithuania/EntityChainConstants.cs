using System;
using System.Collections.Generic;

namespace Viiar.AtmFinder.Core.Repositories.Lithuania
{
    public static class EntityChainConstants
    {
        public const string SwedbankCode = "swedbank";
        public const string Swedbank = "„Swedbank“ Lietuvoje";
        public const string NordeaCode = "nordea";
        public const string Nordea = "Nordea";
        public const string SebCode = "seb";
        public const string Seb = "SEB Lietuvoje";
        public const string DnbCode = "dnb";
        public const string Dnb = "AB DNB bankas";
        public const string CitadeleCode = "citadele";
        public const string Citadele = "AB „Citadele“ bankas";

        internal static readonly Dictionary<string, string> mapping = new Dictionary<string, string>
                                                                         {
                                                                                 { DnbCode, Dnb }, 
                                                                                 { NordeaCode, Nordea }, 
                                                                                 { SebCode, Seb }, 
                                                                                 { SwedbankCode, Swedbank }, 
                                                                                 { CitadeleCode, Citadele }
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

            if (chainName.Equals(Seb, StringComparison.OrdinalIgnoreCase))
            {
                return SebCode;
            }

            if (chainName.Equals(Dnb, StringComparison.OrdinalIgnoreCase))
            {
                return DnbCode;
            }

            if (chainName.Equals(Citadele, StringComparison.OrdinalIgnoreCase))
            {
                return CitadeleCode;
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
