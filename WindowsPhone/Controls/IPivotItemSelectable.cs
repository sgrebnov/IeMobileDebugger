namespace IE.Debug.WindowsPhone.Controls
{
    /// <summary>
    /// Provides methods for Pivot item selected/deselected states.
    /// </summary>
    public interface IPivotItemSelectable
    {
        /// <summary>
        /// Handles pivot item selected event.
        /// </summary>
        void OnSelected();

        /// <summary>
        /// Handles pivot item deselected event.
        /// </summary>
        void OnDeselected();
    }
}
