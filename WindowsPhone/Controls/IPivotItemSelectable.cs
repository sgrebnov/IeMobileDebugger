using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IE.Debug.WindowsPhone.Controls
{
    public interface IPivotItemSelectable
    {
        /// <summary>
        /// Occurs when pivot item is selected.
        /// </summary>
        void OnSelected();

        /// <summary>
        /// Occurs when pivot item is deselected.
        /// </summary>
        void OnDeselected();

    }
}
