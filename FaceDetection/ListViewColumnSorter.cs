using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceDetection {
	class ListViewColumnSorter : IComparer {
		public int ColumnToSort { get; set; }
		public SortOrder OrderOfSort { get; set; }
		private CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();

		public ListViewColumnSorter() {
			ColumnToSort = 0;
			OrderOfSort = SortOrder.None;
		}
		
		public int Compare(object x, object y) {
			int compareResult;
			ListViewItem listviewX, listviewY;
			
			listviewX = (ListViewItem)x;
			listviewY = (ListViewItem)y;

			string valueX = "";
			string valueY = "";
			try {
				valueX = listviewX.SubItems[ColumnToSort].Text;
				valueY = listviewY.SubItems[ColumnToSort].Text;
			} catch (Exception) {
			}
			
			compareResult = ObjectCompare.Compare(valueX, valueY);
			
			if (OrderOfSort == SortOrder.Ascending) {
				return compareResult;
			} else if (OrderOfSort == SortOrder.Descending) {
				return (-compareResult);
			} else {
				return 0;
			}
		}

	}
}