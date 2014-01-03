using System;
using System.Collections.Generic;
using System.Linq;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    public static class FriendlyChainHelper
    {
        private static readonly Dictionary<string, List<string>> chainInfo =
            new Dictionary<string, List<string>>
                {
                    {
                        EntityChainConstants.NordeaCode,
                        new List<string>
                            {
                                EntityChainConstants.CitadeleCode,
                                EntityChainConstants.HipoCode
                            }
                        },
                    {
                        EntityChainConstants.DnbCode,
                        new List<string>
                            {
                                EntityChainConstants.SwedbankCode,
                                EntityChainConstants.SebCode,
                                EntityChainConstants.HipoCode
                            }
                        },
                    {
                        EntityChainConstants.HipoCode,
                        new List<string>
                            {
                                EntityChainConstants.SwedbankCode,
                                EntityChainConstants.SebCode,
                                EntityChainConstants.CitadeleCode,
                                EntityChainConstants.NordeaCode,
                                EntityChainConstants.DnbCode
                            }
                        },
                    {
                        EntityChainConstants.SwedbankCode,
                        new List<string>
                            {
                                EntityChainConstants.DnbCode,
                                EntityChainConstants.SebCode,
                                EntityChainConstants.HipoCode
                            }
                        },
                    {
                        EntityChainConstants.CitadeleCode,
                        new List<string>
                            {
                                EntityChainConstants.HipoCode,
                                EntityChainConstants.NordeaCode
                            }
                        },
                    {
                        EntityChainConstants.SebCode,
                        new List<string>
                            {
                                EntityChainConstants.DnbCode,
                                EntityChainConstants.SwedbankCode,
                                EntityChainConstants.HipoCode,
                                EntityChainConstants.GEMoneyCode
                            }
                        },
                    {
                        EntityChainConstants.GEMoneyCode,
                        new List<string>
                            {
                                EntityChainConstants.SebCode
                            }
                        }
                };

        public static bool AreFirends(List<string> homeBanks, string foreignBank)
        {
            var decoded = RepositoryFactory.DecodeFromSettings(homeBanks);
            return decoded.Any(b => AreFirends(b.Code, foreignBank));
        }

        private static bool AreFirends(string homeBank, string foreignBank)
        {
            if (string.IsNullOrEmpty(homeBank) || string.IsNullOrEmpty(foreignBank))
            {
                return false;
            }

            if (homeBank.Equals(foreignBank, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return chainInfo.ContainsKey(homeBank) && chainInfo[homeBank].Contains(foreignBank);
        }
    }
}
