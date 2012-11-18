#region Using Statements

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

#endregion

namespace FlowDocumentReporting.Engine
{
	internal static class Helpers
	{
		#region Internal Methods

		internal static void FixupDataContext(IBindableContentElement element)
		{
			var contentElement = element as FrameworkContentElement;

			if (contentElement != null)
			{
				Binding b = new Binding(FrameworkContentElement.DataContextProperty.Name);
				b.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(FrameworkElement), 1);
				contentElement.SetBinding(FrameworkContentElement.DataContextProperty, b);
			}
			else
			{
				throw new ArgumentException("The given element must be a FrameworkContentElement", "element");
			}
		}

		internal static void UnFixupDataContext(DependencyObject dp)
		{
			while (InternalUnFixupDataContext(dp))
				;
		}

		internal static Block ConvertToBlock(object dataContext, object data)
		{
			if (data is Block)
			{
				return (Block)data;
			}
			else if (data is Inline)
			{
				return new Paragraph((Inline)data);
			}
			else if (data is BindingBase)
			{
                Run run = new Run();

				if (dataContext is BindingBase)
				{
					run.SetBinding(Run.DataContextProperty, (BindingBase)dataContext);
				}
				else
				{
					run.DataContext = dataContext;
				}

                run.SetBinding(Run.TextProperty, (BindingBase)data);

				return new Paragraph(run);
			}
			else
			{
				Run run = new Run();
				run.Text = (data == null) ? String.Empty : data.ToString();
				return new Paragraph(run);
			}
		}

		internal static FrameworkContentElement LoadDataTemplate(DataTemplate dataTemplate)
		{
			object content = dataTemplate.LoadContent();

			if (content is Fragment)
			{
				return (FrameworkContentElement)((Fragment)content).Content;
			}
			else if (content is TextBlock)
			{
				InlineCollection inlines = ((TextBlock)content).Inlines;

				if (inlines.Count == 1)
				{
					return inlines.FirstInline;
				}
				else
				{
					Paragraph paragraph = new Paragraph();

					while (inlines.FirstInline != null)
					{
						paragraph.Inlines.Add(inlines.FirstInline);
					}

					return paragraph;
				}
			}
			else
			{
				throw new Exception("Data template needs to contain a <Fragment> or <TextBlock>");
			}
		}

		internal static double CentimeterToDeviceIndependentUnit(double centimeters)
		{
			return centimeters * 96.0 / 2.54;
		}

		internal static Size SubtractSize(Size size1, Size size2)
		{
			if (size1 == Size.Empty)
			{
				throw new ArgumentException("Size cannot be empty", "size1");
			}

			if (size2 == Size.Empty)
			{
				throw new ArgumentException("Size cannot be empty", "size2");
			}

			if (size1.Height < size2.Height)
			{
				throw new ArgumentException("First height must be equal or greater than second height", "size1");
			}

			if (size1.Width < size2.Width)
			{
				throw new ArgumentException("First width must be equal or greater than second width", "size1");
			}

			return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
		}

		#endregion
                                                           
		#region Private Methods

		private static bool InternalUnFixupDataContext(DependencyObject dp)
		{
			if (dp is IBindableContentElement)
			{
				Binding binding = BindingOperations.GetBinding(dp, FrameworkContentElement.DataContextProperty);

				if (binding != null
					&& binding.Path != null
					&& binding.Path.Path == FrameworkContentElement.DataContextProperty.Name
					&& binding.RelativeSource != null
					&& binding.RelativeSource.Mode == RelativeSourceMode.FindAncestor
					&& binding.RelativeSource.AncestorType == typeof(FrameworkElement)
					&& binding.RelativeSource.AncestorLevel == 1)
				{
					BindingOperations.ClearBinding(dp, FrameworkContentElement.DataContextProperty);
					return true;
				}
			}

			foreach (object child in LogicalTreeHelper.GetChildren(dp))
			{
				if (child is DependencyObject)
				{
					if (InternalUnFixupDataContext((DependencyObject)child))
					{
						return true;
					}
				}
			}

			return false;
		}

		#endregion
	}
}