﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.SampleDataSource
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class SampleDataSource { }
#else

	public class SampleDataSource : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public SampleDataSource()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/Viiar.AtmFinder.UI;component/SampleData/SampleDataSource/SampleDataSource.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private string _Title = string.Empty;

		public string Title
		{
			get
			{
				return this._Title;
			}

			set
			{
				if (this._Title != value)
				{
					this._Title = value;
					this.OnPropertyChanged("Title");
				}
			}
		}

		private string _Address = string.Empty;

		public string Address
		{
			get
			{
				return this._Address;
			}

			set
			{
				if (this._Address != value)
				{
					this._Address = value;
					this.OnPropertyChanged("Address");
				}
			}
		}

		private string _Description = string.Empty;

		public string Description
		{
			get
			{
				return this._Description;
			}

			set
			{
				if (this._Description != value)
				{
					this._Description = value;
					this.OnPropertyChanged("Description");
				}
			}
		}

		private string _CoordinatesDisplay = string.Empty;

		public string CoordinatesDisplay
		{
			get
			{
				return this._CoordinatesDisplay;
			}

			set
			{
				if (this._CoordinatesDisplay != value)
				{
					this._CoordinatesDisplay = value;
					this.OnPropertyChanged("CoordinatesDisplay");
				}
			}
		}

		private System.Windows.Media.ImageSource _Icon = null;

		public System.Windows.Media.ImageSource Icon
		{
			get
			{
				return this._Icon;
			}

			set
			{
				if (this._Icon != value)
				{
					this._Icon = value;
					this.OnPropertyChanged("Icon");
				}
			}
		}
	}
#endif
}
