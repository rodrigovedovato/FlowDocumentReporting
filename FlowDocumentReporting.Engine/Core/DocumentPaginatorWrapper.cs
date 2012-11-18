#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

#endregion

namespace FlowDocumentReporting.Engine
{
	public class DocumentPaginatorWrapper : DocumentPaginator
	{
		#region Member Variables

		private DocumentPaginator _paginator = null;
		private PrintableDocumentDefinition _definition = null;
		private PrintableDocument doc = null;

		#endregion

		#region Public Properties

		public override bool IsPageCountValid
		{
			get
			{
				return _paginator.IsPageCountValid;
			}
		}

		public override int PageCount
		{
			get
			{
				return _paginator.PageCount;
			}
		}

		public override IDocumentPaginatorSource Source
		{
			get
			{
				return _paginator.Source;
			}
		}

		public override Size PageSize
		{
			get;
			set;
		}

		#endregion

		#region Construction and Initialization

		public DocumentPaginatorWrapper(DocumentPaginator paginator, PrintableDocumentDefinition definition)
		{
			if (!(paginator.Source is PrintableDocument))
			{
				throw new Exception("This paginator can be used only in PrintableDocument classes");
			}            

			_paginator = paginator;
			_definition = definition;

            doc = Source as PrintableDocument;

			var headerSize = GetModelContentSize(doc.DataContext, doc.Header, _definition.PageSize);
			var footerSize = GetModelContentSize(doc.DataContext, doc.Footer, _definition.PageSize);

			_definition.HeaderHeight = double.IsInfinity(headerSize.Height) ? 0 : headerSize.Height;
			_definition.FooterHeight = double.IsInfinity(footerSize.Height) ? 0 : footerSize.Height;

			_paginator.PageSize = _definition.ContentSize;

            ComputePageCount();

            doc.DataSource.PageCount = _paginator.PageCount;
		}

		#endregion

		#region Public Methods        

		public override DocumentPage GetPage(int pageNumber)
		{
			var page = _paginator.GetPage(pageNumber);
			Visual originalPage = page.Visual;

			var visual = new ContainerVisual();

            doc.DataSource.PageNumber = pageNumber + 1;
            
			var headerVisual = GetHeaderFooterVisualFromDataTemplate(doc.Header, _definition.HeaderRect);
			var footerVisual = GetHeaderFooterVisualFromDataTemplate(doc.Footer, _definition.FooterRect);            

			headerVisual.Offset = new Vector(_definition.HeaderRect.X, _definition.HeaderRect.Y);
			footerVisual.Offset = new Vector(_definition.FooterRect.X, _definition.FooterRect.Y);

			visual.Children.Add(headerVisual);
			visual.Children.Add(footerVisual);

			var pageVisual = new ContainerVisual()
			{
				Transform = new TranslateTransform(_definition.ContentOrigin.X, _definition.ContentOrigin.Y),
			};

			pageVisual.Children.Add(originalPage);
			visual.Children.Add(pageVisual);

			return new DocumentPage(visual, _definition.PageSize, new Rect(new Point(), _definition.ContentSize), new Rect(_definition.ContentOrigin, _definition.PageSize));
		}

		#endregion

		#region Private Methods

		private ContainerVisual GetHeaderFooterVisualFromDataTemplate(FrameworkTemplate template, Rect container)
		{
			if (template == null)
			{
				return null;
			}

			var doc = Source as PrintableDocument;

			var content = template.LoadContent() as FrameworkElement;
			content.DataContext = doc.DataContext;

			var width = _definition.PageSize.Width - _definition.Margins.Left - _definition.Margins.Right;

			content.Measure(_definition.PageSize);
			content.Arrange(new Rect(container.X, container.Y, width, content.DesiredSize.Height));
			content.UpdateLayout();

			var visual = new ContainerVisual();

			visual.Children.Add(content);

			return visual;
		}

		private Size GetModelContentSize(object dataModel, FrameworkTemplate modelTemplate, Size availableSize)
		{
			FrameworkElement content = null;

			if (modelTemplate == null)
			{
				return Size.Empty;
			}

			content = modelTemplate.LoadContent() as FrameworkElement;

			if (content == null)
			{
				return Size.Empty;
			}

			content.DataContext = dataModel;
			content.Measure(availableSize);

			return content.DesiredSize;
		}

		#endregion
	}
}