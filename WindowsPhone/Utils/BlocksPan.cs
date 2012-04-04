/*
 *  http://blogs.msdn.com/b/luc/archive/2010/11/22/preventing-the-pivot-or-panorama-controls-from-scrolling.aspx?wa=wsignin1.0
 */

using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace IE.Debug.WindowsPhone.Controls
{
    public class BlocksPan
    {
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabled);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabled, value);
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabled =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(BlocksPan), new PropertyMetadata(false, IsEnabled_DependencyPropertyChangedCallback));

        // Contains the Panorama/Pivot. This extension cannot handle blocking
        // multiple panning controls simultaneously.
        static FrameworkElement InternalPanningControl { get; set; }

        static void IsEnabled_DependencyPropertyChangedCallback(DependencyObject dobj, DependencyPropertyChangedEventArgs ea)
        {
            // The element that should prevent the Panorama/Pivot from scrolling
            var blockingElement = dobj as FrameworkElement;

            blockingElement.Unloaded += blockingElement_Unloaded;
            blockingElement.MouseLeftButtonDown += SuspendScroll;
            blockingElement.ManipulationStarted += SuspendScroll;
        }

        static void blockingElement_Unloaded(object sender, RoutedEventArgs e)
        {
            var fel = sender as FrameworkElement;
            fel.Unloaded -= blockingElement_Unloaded;
            fel.MouseLeftButtonDown -= SuspendScroll;
        }

        static void SuspendScroll(object sender, RoutedEventArgs e)
        {
            var blockingElement = sender as FrameworkElement;

            #region Initialization
            // Determines the parent Panorama/Pivot control
            if (InternalPanningControl == null)
                InternalPanningControl = BlocksPan.FindAncestor(blockingElement, p => (p is Panorama || p is Pivot)) as FrameworkElement;
            #endregion

            var isAlreadySuspended = (bool)(sender as FrameworkElement).GetValue(BlocksPan.IsScrollSuspendedProperty);
            if (isAlreadySuspended) return;

            // When the user touches the control...
            var originalSource = e.OriginalSource as DependencyObject;
            if (BlocksPan.FindAncestor(originalSource, dobj => (dobj == blockingElement)) == blockingElement)
            {
                // Mark the parent Panorama/Pivot for scroll suspension
                // and register for touch frame events
                InternalPanningControl.SetValue(BlocksPan.IsScrollSuspendedProperty, true);
                Touch.FrameReported += Touch_FrameReported;
                blockingElement.IsHitTestVisible = true;
                InternalPanningControl.IsHitTestVisible = false;
            }
        }

        private static void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            // (When the parent Panorama/Pivot is suspended)
            // Wait for the first touch to end (touchaction up). When it is, restore standard
            // panning behavior, otherwise let the control behave normally (no code for this)
            var lastTouchPoint = InternalPanningControl.GetValue(BlocksPan.LastTouchPointProperty) as TouchPoint;
            var isScrollSuspended = (bool)InternalPanningControl.GetValue(BlocksPan.IsScrollSuspendedProperty);
            var touchPoints = e.GetTouchPoints(InternalPanningControl);

            if (lastTouchPoint != touchPoints.Last() || lastTouchPoint == null)
                lastTouchPoint = touchPoints.Last();

            if (isScrollSuspended)
            {
                // Touch is up, custom behavior is over reset to original values
                if (lastTouchPoint.Action == TouchAction.Up)
                {
                    Touch.FrameReported -= Touch_FrameReported;
                    lastTouchPoint = null;
                    InternalPanningControl.IsHitTestVisible = true;
                    isScrollSuspended = false;
                }
            }
        }

        # region IsScrollSuspended, LastTouchPoint dps, BlockingElements

        private static bool GetIsScrollSuspended(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsScrollSuspendedProperty);
        }

        private static void SetIsScrollSuspended(DependencyObject obj, bool value)
        {
            obj.SetValue(IsScrollSuspendedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsScrollSuspended.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty IsScrollSuspendedProperty =
            DependencyProperty.RegisterAttached("IsScrollSuspended", typeof(bool), typeof(BlocksPan), new PropertyMetadata(false));

        private static TouchPoint GetLastTouchPoint(DependencyObject obj)
        {
            return (TouchPoint)obj.GetValue(LastTouchPointProperty);
        }

        private static void SetLastTouchPoint(DependencyObject obj, TouchPoint value)
        {
            obj.SetValue(LastTouchPointProperty, value);
        }

        // Using a DependencyProperty as the backing store for LastTouchPoint.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty LastTouchPointProperty =
            DependencyProperty.RegisterAttached("LastTouchPoint", typeof(TouchPoint), typeof(BlocksPan), new PropertyMetadata(null));
        #endregion

        /// <summary>
        /// Traverses the Visual Tree upwards looking for the ancestor that satisfies the <paramref name="predicate"/>.
        /// </summary>
        /// <param name="dependencyObject">The element for which the ancestor is being looked for.</param>
        /// <param name="predicate">The predicate that evaluates if an element is the ancestor that is being looked for.</param>
        /// <returns>
        /// The ancestor element that matches the <paramref name="predicate"/> or <see langword="null"/>
        /// if the ancestor was not found.
        /// </returns>
        public static DependencyObject FindAncestor(DependencyObject dependencyObject, Func<DependencyObject, bool> predicate)
        {
            if (predicate(dependencyObject))
            {
                return dependencyObject;
            }

            DependencyObject parent = null;
            FrameworkElement frameworkElement = dependencyObject as FrameworkElement;
            if (frameworkElement != null)
            {
                parent = frameworkElement.Parent ?? System.Windows.Media.VisualTreeHelper.GetParent(frameworkElement);
            }
            if (parent != null)
            {
                return FindAncestor(parent, predicate);
            }

            return null;
        }

    }
}
