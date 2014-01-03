using Viiar.AtmFinder.W8.Common;

namespace Viiar.AtmFinder.W8
{
    public class MyBankViewModel : BindableBase
    {
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                SetProperty(ref this.isSelected, value);
            }
        }

        public string Name { get; set; }
        public string Code { get; set; }
    }
}
