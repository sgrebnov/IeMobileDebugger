//-----------------------------------------------------------------------
// <copyright file="Messages.cs" company="Akvelon">
//     Copyright (c) Akvelon. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

using System;
using System.Windows;

namespace Support
{
    /// <summary>
    /// Provides message showing functionality 
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Displays error message box that contains the specified text.
        /// </summary>
        /// <param name="text">Text to show.</param>
        public static void ShowError(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", "message can't be null");
            }

            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(text, "Sorry, an error occurred", MessageBoxButton.OK));
        }

        /// <summary>
        /// Displays message box that contains the specified text.
        /// </summary>
        /// <param name="text">Text to show.</param>
        public static void ShowYesCancel(string text, Action onOk, Action onCancel)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", "message can't be null");
            }

            // TODO 'HealthGuard' to constants
            MessageBoxResult res = MessageBox.Show(text, "HealthGuard", MessageBoxButton.OKCancel);

            if (res == MessageBoxResult.OK && onOk != null)
            {
                onOk();
                return;
            }

            if (res == MessageBoxResult.Cancel && onCancel != null)
            {
                onCancel();
                return;
            }
        }

        /// <summary>
        /// Displays message box that contains the specified text.
        /// </summary>
        /// <param name="text">Text to show.</param>
        public static void ShowYes(string text, Action onOk)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", "message can't be null");
            }

            // TODO 'HealthGuard' to constants
            MessageBoxResult res = MessageBox.Show(text, "HealthGuard", MessageBoxButton.OK);

            if (res == MessageBoxResult.OK && onOk != null)
            {
                onOk();
                return;
            }
        }
    }
}