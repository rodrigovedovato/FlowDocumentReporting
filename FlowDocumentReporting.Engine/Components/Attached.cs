#region Using Statements

using System;
using System.Windows;

#endregion

namespace FlowDocumentReporting.Engine
{
	public class Attached
	{
		#region Static Variables

		private static readonly DependencyProperty IsItemsHostProperty =
			DependencyProperty.RegisterAttached("IsItemsHost", typeof(bool), typeof(Attached), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.NotDataBindable, OnIsItemsHostChanged));

		private static readonly DependencyProperty ItemsHostProperty =
			DependencyProperty.RegisterAttached("ItemsHost", typeof(FrameworkContentElement), typeof(Attached), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.NotDataBindable));

		#endregion

		#region Public Methods

		public static bool GetIsItemsHost(DependencyObject target)
		{
			return (bool)target.GetValue(IsItemsHostProperty);
		}

		public static void SetIsItemsHost(DependencyObject target, bool value)
		{
			target.SetValue(IsItemsHostProperty, value);
		}

		public static FrameworkContentElement GetItemsHost(DependencyObject dp)
		{
			return (FrameworkContentElement)dp.GetValue(ItemsHostProperty);
		}

		#endregion

		#region Private Methods

		private static void SetItemsHost(FrameworkContentElement element)
		{
			FrameworkContentElement parent = element;

			while (parent.Parent != null)
			{
				parent = (FrameworkContentElement)parent.Parent;
			}

			parent.SetValue(ItemsHostProperty, element);
		}

		private static void OnIsItemsHostChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue)
			{
				FrameworkContentElement element = (FrameworkContentElement)d;

				if (element.IsInitialized)
				{
					SetItemsHost(element);
				}
				else
				{
					element.Initialized += ItemsHost_Initialized;
				}
			}
		}

		private static void ItemsHost_Initialized(object sender, EventArgs e)
		{
			FrameworkContentElement element = (FrameworkContentElement)sender;
			element.Initialized -= ItemsHost_Initialized;
			SetItemsHost(element);
		}

		#endregion
	}
}