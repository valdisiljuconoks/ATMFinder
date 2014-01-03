using System;
using System.Collections.Generic;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    public static class EntityChainConstants
    {
        public const string Citadele = "Citadele";
        public const string CitadeleCode = "citadele";
        public const string Dnb = "DNB";
        public const string DnbCode = "dnb";
        public const string GEMoney = "GE Money";
        public const string GEMoneyCode = "gemoney";
        public const string Hipo = "Hipotēku banka";
        public const string HipoCode = "hipo";
        public const string Nordea = "Nordea";
        public const string NordeaCode = "nordea";
        public const string Seb = "SEB";
        public const string SebCode = "seb";
        public const string Swedbank = "Swedbank";
        public const string SwedbankCode = "swedbank";

        internal static readonly Dictionary<string, string> mapping = new Dictionary<string, string>
                                                                         {
                                                                                 { CitadeleCode, Citadele }, 
                                                                                 { DnbCode, Dnb }, 
                                                                                 { GEMoneyCode, GEMoney }, 
                                                                                 { HipoCode, Hipo }, 
                                                                                 { NordeaCode, Nordea }, 
                                                                                 { SebCode, Seb }, 
                                                                                 { SwedbankCode, Swedbank }
                                                                         };

        public static string GetCode(string chainName)
        {
            if (string.IsNullOrEmpty(chainName))
            {
                throw new ArgumentNullException("chainName");
            }

            if (chainName.Equals(Hipo, StringComparison.OrdinalIgnoreCase))
            {
                return HipoCode;
            }

            if (chainName.Equals(GEMoney, StringComparison.OrdinalIgnoreCase))
            {
                return GEMoneyCode;
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
