using Viiar.AtmFinder.W8.Common;

namespace Viiar.AtmFinder.W8
{
    /// <summary>
    ///   View model describing one of the filters available for viewing search results.
    /// </summary>
    internal sealed class Filter : BindableBase
    {
        private bool _active;
        private int _count;
        private string _name;

        public Filter(string name, int count, bool active = false)
        {
            Name = name;
            Count = count;
            Active = active;
        }

        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                if (SetProperty(ref this._name, value))
                {
                    OnPropertyChanged("Description");
                }
            }
        }

        public int Count
        {
            get
            {
                return this._count;
            }

            set
            {
                if (SetProperty(ref this._count, value))
                {
                    OnPropertyChanged("Description");
                }
            }
        }

        public bool Active
        {
            get
            {
                return this._active;
            }

            set
            {
                SetProperty(ref this._active, value);
            }
        }

        public string Description
        {
            get
            {
                return string.Format("{0} ({1})", this._name, this._count);
            }
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
