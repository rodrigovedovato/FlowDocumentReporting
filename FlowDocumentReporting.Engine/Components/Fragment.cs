#region Using Statements

using System.Windows;
using System.Windows.Markup;

#endregion

namespace FlowDocumentReporting.Engine
{
	[ContentProperty("Content")]
	public class Fragment : FrameworkElement
	{
		#region Static Variables

        private static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(FrameworkContentElement), typeof(Fragment));

		#endregion

		#region Public Properties

		public FrameworkContentElement Content
		{
			get
			{
				return (FrameworkContentElement)GetValue(ContentProperty);
			}
			set
			{
				SetValue(ContentProperty, value);
			}
		}

		#endregion
    }
}