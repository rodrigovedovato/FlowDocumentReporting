#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

#endregion

namespace FlowDocumentReporting.Engine
{
	public class PrintableDocument : FlowDocument
	{
		#region Static Variables

		public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(DataTemplate), typeof(PrintableDocument), new PropertyMetadata());
		public static readonly DependencyProperty FooterProperty = DependencyProperty.Register("Footer", typeof(DataTemplate), typeof(PrintableDocument), new PropertyMetadata());

		#endregion

		#region Public Properties

		public DataTemplate Header
		{
			get
			{
				return GetValue(HeaderProperty) as DataTemplate;
			}
			set
			{
				SetValue(HeaderProperty, value);
			}
		}

		public DataTemplate Footer
		{
			get
			{
				return GetValue(FooterProperty) as DataTemplate;
			}
			set
			{
				SetValue(FooterProperty, value);
			}
		}

		public DataSourceBase DataSource
		{
			get
			{
				return DataContext as DataSourceBase;
			}
			set
			{
				DataContext = value;
			}
		}

		#endregion

		#region Events and Delegates

		public delegate void PrintableDocumentEvent(PrintableDocument printableDocument);

		#endregion

		#region Construction and Initialization

		public PrintableDocument()
		{
			var resource = new ResourceDictionary();
			resource.Source = new Uri(@"pack://application:,,,/Valor.Managed.Vip.Themes.DefaultTheme;component/Controls/Printing.xaml");

			Header = resource["DocumentHeaderTemplate"] as DataTemplate;
			Footer = resource["DocumentFooterTemplate"] as DataTemplate;
		}

		#endregion
	}
}