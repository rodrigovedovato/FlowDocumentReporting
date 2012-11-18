#region Using Statements

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace FlowDocumentReporting.Engine
{
	[ContentProperty("Base64Source")]
	public class BitmapImageContainer : BlockUIContainer, IBindableContentElement
	{
		#region Static Variables

		public static readonly DependencyProperty WidthProperty = 
			DependencyProperty.Register("Width", typeof(double), typeof(BitmapImageContainer), new FrameworkPropertyMetadata(Double.NaN));
		public static readonly DependencyProperty HeightProperty = 
			DependencyProperty.Register("Height", typeof(double), typeof(BitmapImageContainer), new FrameworkPropertyMetadata(Double.NaN));
		public static readonly DependencyProperty StretchProperty = 
			DependencyProperty.Register("Stretch", typeof(Stretch), typeof(BitmapImageContainer), new FrameworkPropertyMetadata(Stretch.Uniform));
		public static readonly DependencyProperty StretchDirectionProperty = 
			DependencyProperty.Register("StretchDirection", typeof(StretchDirection), typeof(BitmapImageContainer), new FrameworkPropertyMetadata(StretchDirection.Both));
		public static readonly DependencyProperty Base64SourceProperty = 
			DependencyProperty.Register("Base64Source", typeof(string), typeof(BitmapImageContainer), new FrameworkPropertyMetadata(null, OnBase64SourceChanged));

		#endregion

		#region Public Properties

		public double Width
		{
			get { return (double)GetValue(WidthProperty); }
			set { SetValue(WidthProperty, value); }
		}

		public double Height
		{
			get { return (double)GetValue(HeightProperty); }
			set { SetValue(HeightProperty, value); }
		}

		public Stretch Stretch
		{
			get { return (Stretch)GetValue(StretchProperty); }
			set { SetValue(StretchProperty, value); }
		}

		public StretchDirection StretchDirection
		{
			get { return (StretchDirection)GetValue(StretchDirectionProperty); }
			set { SetValue(StretchDirectionProperty, value); }
		}

		public string Base64Source
		{
			get { return (string)GetValue(Base64SourceProperty); }
			set { SetValue(Base64SourceProperty, value); }
		}

		#endregion

		#region Construction and Initialization

		public BitmapImageContainer()
		{
			Helpers.FixupDataContext(this);
		}

		#endregion

		#region Private Methods

		private static void OnBase64SourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var imageContainer = (BitmapImageContainer)sender;
			BitmapImage bitmapImage = null;

			var stream = new MemoryStream(Convert.FromBase64String(imageContainer.Base64Source));
			bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.StreamSource = stream;
			bitmapImage.EndInit();

			var image = new Image();

			image.Source = bitmapImage;
			image.Stretch = imageContainer.Stretch;
			image.StretchDirection = imageContainer.StretchDirection;

			if (!double.IsNaN(imageContainer.Width))
			{
				image.Width = imageContainer.Width;
			}

			if (!double.IsNaN(imageContainer.Height))
			{
				image.Height = imageContainer.Height;
			}

			imageContainer.Child = image;
		}

		#endregion
	}
}