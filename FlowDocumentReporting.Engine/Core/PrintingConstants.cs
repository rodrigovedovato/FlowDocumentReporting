#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace FlowDocumentReporting.Engine
{
	public class PrintingConstants
	{
		#region Constants

		public const double A4_PAGE_WIDTH = 793.7007874015748;
		public const double A4_PAGE_HEIGHT = 1122.51968503937;
		public const int INCHES_TO_DIU_CONVERSION_FACTOR = 96;
		public const double CM_TO_INCHES_CONVERSION_FACTOR = 0.39370078740157480315;

		#endregion

		#region Static Variables

		public static double DEFAULT_LEFT_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1);
		public static double DEFAULT_RIGHT_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1);
		public static double DEFAULT_TOP_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1);
		public static double DEFAULT_BOTTOM_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1.5);
		public static double DEFAULT_LEFT_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0);
		public static double DEFAULT_TOP_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0.8);
		public static double DEFAULT_RIGHT_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0);
		public static double DEFAULT_BOTTOM_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0.8);
		public static double DEFAULT_HORIZONTAL_LEFT_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1);
		public static double DEFAULT_HORIZONTAL_RIGHT_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1);
		public static double DEFAULT_HORIZONTAL_TOP_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1);
		public static double DEFAULT_HORIZONTAL_BOTTOM_MARGIN = Helpers.CentimeterToDeviceIndependentUnit(1);
		public static double DEFAULT_HORIZONTAL_LEFT_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0);
		public static double DEFAULT_HORIZONTAL_TOP_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0.2);
		public static double DEFAULT_HORIZONTAL_RIGHT_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0);
		public static double DEFAULT_HORIZONTAL_BOTTOM_CONTENT_PADDING = Helpers.CentimeterToDeviceIndependentUnit(0.2);

		#endregion
	}
}