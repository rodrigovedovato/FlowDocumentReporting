#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


#endregion

namespace FlowDocumentReporting.Engine
{
    public class DataSourceBase
    {
        #region Member Variables

        private DateTime _printDate = DateTime.Now;
        private PrintableDocumentDefinition _printableDocumentDefinition = new PrintableDocumentDefinition();
        private string _phone;
        private string _copyright;

        #endregion

        #region Public Properties

        public PrintableDocumentDefinition PrintableDocumentDefinition
        {
            get
            {
                return _printableDocumentDefinition;
            }
            set
            {
                _printableDocumentDefinition = value;
            }
        }

        public int PageNumber
        {
            get;
            internal set;
        }

        private int _pagecount;
        public int PageCount
        {
            get { return _pagecount; }
            internal set { _pagecount = value; }
        }

        public DateTime PrintDate
        {
            get
            {
                return _printDate;
            }
        }

        public string ToolName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Copyright
        {
            get;
            set;
        }

        #endregion

        #region Construction and Initialization

        public DataSourceBase()
        {
        }

        #endregion
    }
}