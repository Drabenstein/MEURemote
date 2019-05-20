using System;
using System.Collections;
using System.Windows.Forms;

namespace Remote
{
    public class ListViewItemComparer : IComparer
    {
        private int _col;
        private SortOrder _order;

        public ListViewItemComparer()
        {
            _col = 0;
            _order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            _col = column;
            _order = order;
        }

        public int Compare(object x, object y)
        {
            int returnValue = -1;
            returnValue = String.Compare(((ListViewItem)x).SubItems[_col].Text,
                ((ListViewItem)y).SubItems[_col].Text);
            if (_order == SortOrder.Descending)
            {
                // Invert value returned by String.Compare
                returnValue *= -1;
            }
            return returnValue;
        }
    }
}
