#region Using Statements

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Draw = System.Drawing;

#endregion

namespace FlowDocumentReporting.Engine
{
	public class PrintableDocumentDefinition : INotifyPropertyChanged
	{
		#region Member Variables

		private readonly Size _verticalOrientationPageSize;
		private readonly Size _horizontalOrientationPageSize;
		private readonly Thickness _verticalOrientationMargins;
		private readonly Thickness _horizontalOrientationMargins;
		private readonly Thickness _verticalOrientationContentPadding;
		private readonly Thickness _horizontalOrientationContentPadding;
		private Size _pageSize;
		private Thickness _margins;
		private Thickness _contentPadding;
		private Orientation _pageOrientation = Orientation.Vertical;

		#endregion

		#region Internal Properties

		internal double HeaderHeight
		{
			get;
			set;
		}

		internal double FooterHeight
		{
			get;
			set;
		}

		internal Size ContentSize
		{
			get
			{
				return Helpers.SubtractSize(PageSize, new Size(_margins.Left + _margins.Right,
					_margins.Top + _margins.Bottom + HeaderHeight + FooterHeight + _contentPadding.Bottom + _contentPadding.Top));
			}
		}

		internal Point ContentOrigin
		{
			get
			{
				return new Point(
					_margins.Left + _contentPadding.Left,
					_margins.Top + HeaderRect.Height + _contentPadding.Top
				);
			}
		}

		internal Rect HeaderRect
		{
			get
			{
				return new Rect(
					_margins.Left, _margins.Top,
					ContentSize.Width, HeaderHeight
				);
			}
		}

		internal Rect FooterRect
		{
			get
			{
				return new Rect(
					_margins.Left, ContentOrigin.Y + ContentSize.Height + _contentPadding.Bottom,
					ContentSize.Width, FooterHeight
				);
			}
		}

		#endregion

		#region Public Properties

		public Size PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				if (_pageSize != value)
				{
					_pageSize = value;
					OnPropertyChanged("PageSize");
				}
			}
		}

		public Thickness Margins
		{
			get
			{
				return _margins;
			}
			set
			{
				if (_margins != value)
				{
					_margins = value;
					OnPropertyChanged("Margins");
				}
			}
		}

		public Thickness ContentPadding
		{
			get
			{
				return _contentPadding;
			}
			set
			{
				if (_contentPadding != value)
				{
					_contentPadding = value;
					OnPropertyChanged("ContentPadding");
				}
			}
		}

		public Orientation PageOrientation
		{
			get
			{
				return _pageOrientation;
			}
			set
			{
				if (_pageOrientation != value)
				{
					_pageOrientation = value;

					ChangePageOrientation();

					OnPropertyChanged("PageOrientation");
				}
			}
		}

		#endregion

		#region Events and Delegates

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Construction and Initialization

		public PrintableDocumentDefinition()
		{
			_verticalOrientationPageSize = new Size(PrintingConstants.A4_PAGE_WIDTH, PrintingConstants.A4_PAGE_HEIGHT);
			_horizontalOrientationPageSize = new Size(PrintingConstants.A4_PAGE_HEIGHT, PrintingConstants.A4_PAGE_WIDTH);

			_verticalOrientationMargins = new Thickness(PrintingConstants.DEFAULT_LEFT_MARGIN, PrintingConstants.DEFAULT_TOP_MARGIN, PrintingConstants.DEFAULT_RIGHT_MARGIN, PrintingConstants.DEFAULT_BOTTOM_MARGIN);
			_horizontalOrientationMargins = new Thickness(PrintingConstants.DEFAULT_HORIZONTAL_LEFT_MARGIN, PrintingConstants.DEFAULT_HORIZONTAL_TOP_MARGIN, PrintingConstants.DEFAULT_HORIZONTAL_RIGHT_MARGIN, PrintingConstants.DEFAULT_HORIZONTAL_BOTTOM_MARGIN);

			_verticalOrientationContentPadding = new Thickness(PrintingConstants.DEFAULT_LEFT_CONTENT_PADDING, PrintingConstants.DEFAULT_TOP_CONTENT_PADDING, PrintingConstants.DEFAULT_RIGHT_CONTENT_PADDING, PrintingConstants.DEFAULT_BOTTOM_CONTENT_PADDING);
			_horizontalOrientationContentPadding = new Thickness(PrintingConstants.DEFAULT_HORIZONTAL_LEFT_CONTENT_PADDING, PrintingConstants.DEFAULT_HORIZONTAL_TOP_CONTENT_PADDING, PrintingConstants.DEFAULT_HORIZONTAL_RIGHT_CONTENT_PADDING, PrintingConstants.DEFAULT_HORIZONTAL_BOTTOM_CONTENT_PADDING);

			_pageSize = _verticalOrientationPageSize;
			_margins = _verticalOrientationMargins;
			_contentPadding = _verticalOrientationContentPadding;

			ChangePageOrientation();
		}

		#endregion

		#region Private Methods

		private void ChangePageOrientation()
		{
			switch (_pageOrientation)
			{
			case Orientation.Horizontal:
				_pageSize = _horizontalOrientationPageSize;
				_margins = _horizontalOrientationMargins;
				_contentPadding = _horizontalOrientationContentPadding;
				break;
			case Orientation.Vertical:
			default:
				_pageSize = _verticalOrientationPageSize;
				_margins = _verticalOrientationMargins;
				_contentPadding = _verticalOrientationContentPadding;
				break;
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}