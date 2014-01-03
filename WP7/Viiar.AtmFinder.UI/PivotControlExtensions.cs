using System;
using System.Linq;
using Microsoft.Phone.Controls;

namespace Viiar.AtmFinder.UI
{
    public static class PivotControlExtensions
    {
        public static int FindItemIndex(this Pivot pivot, string name)
        {
            return pivot.Items.TakeWhile(t => !((PivotItem)t).Tag.ToString().Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count();
        }
    }
}
