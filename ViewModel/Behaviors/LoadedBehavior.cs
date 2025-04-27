using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace ViewModel.Behaviors
{
	public static class LoadedBehavior
	{
		public static readonly DependencyProperty CommandProperty =
		   DependencyProperty.RegisterAttached(
			   "Command",
			   typeof(ICommand),
			   typeof(LoadedBehavior),
			   new PropertyMetadata(null, OnCommandChanged));

		public static void SetCommand(DependencyObject target, ICommand value)
		{
			target.SetValue(CommandProperty, value);
		}

		public static ICommand GetCommand(DependencyObject target)
		{
			return (ICommand)target.GetValue(CommandProperty);
		}

		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement element)
			{
				element.Loaded -= OnLoaded;
				if (e.NewValue != null)
				{
					element.Loaded += OnLoaded;
				}
			}
		}

		private static void OnLoaded(object sender, RoutedEventArgs e)
		{
			var element = (FrameworkElement)sender;
			var command = GetCommand(element);
			if (command?.CanExecute(null) == true)
			{
				command.Execute(null);
			}
		}
	}
}
