using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DemoForms140.ViewModels
{
	public class ListItemViewModel : INotifyPropertyChanged
	{
		public ListItemViewModel()
		{
			BackgroundColor = Color.White;
			TextColor = Color.Accent;
		}

		private int _id;
		public int Id 
		{ 
			get { return _id; } 
			set { SetField(ref _id, value); } 
		}

		private string _text;
		public string Text 
		{ 
			get { return _text; } 
			set { SetField(ref _text, value); } 
		}

		private Color _backgroundColor;
		public Color BackgroundColor 
		{ 
			get { return _backgroundColor; } 
			set { SetField(ref _backgroundColor, value); } 
		}

		private Color _textColor;
		public Color TextColor 
		{ 
			get { return _textColor; } 
			set { SetField(ref _textColor, value); } 
		}

		#region Wire-up the Observable parts
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
		protected bool SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, newValue))
			{
				return false;
			}

			field = newValue;

			OnPropertyChanged(propertyName);

			return true;
		}
		#endregion
	}
}

