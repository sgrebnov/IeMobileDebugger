//-----------------------------------------------------------------------
// <copyright file="ExceptionHandler.cs" company="Akvelon">
//     Copyright (c) Akvelon. All rights reserved.
// </copyright>
//----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;

namespace Support
{
    /// <summary>
    /// Implements exception information logging.
    /// </summary>
    public static class ExceptionHandler
    {
        #region Constants

        /// <summary>
        ///  Name of the file to save information in.
        /// </summary>
        public const string ExceptionLogFileName = "exceptions.txt";

        #endregion

        #region Fields

        /// <summary>
        /// Fully qualified path of Log file
        /// </summary>
        private static readonly string logFilePath;

        #endregion

        #region .ctor

        /// <summary>
        /// Initializes static members of the ExceptionHandler class.
        /// </summary>
        static ExceptionHandler()
        {
            logFilePath = ExceptionLogFileName;
        }
        #endregion

        #region Public functionality

        /// <summary>
        /// Saves string message to log
        /// </summary>
        /// <param name="message">Message to put into log</param>
        public static void Log(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                Debug.Assert(false, "Invalid argument at Log");
                return;
            }

            try
            {
                string logData = String.Format("{0} (In UTC: {1}) => {2}", DateTime.Now, DateTime.UtcNow, message);

                DataToFile(logData);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves string message to log and then shows message to user
        /// </summary>
        /// <param name="message">Message to put into log</param>
        public static void LogAndNotify(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                Debug.Assert(false, "Invalid argument at LogAndNotify");
                return;
            }

            try
            {
                Log(message);
                Messages.ShowError(message);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves exception occurred to log.
        /// </summary>
        /// <param name="ex">Exception to save information about</param>
        public static void Log(Exception ex)
        {
            if (ex == null)
            {
                Debug.Assert(false, "Invalid argument at Log");
                return;
            }

            try
            {
                string exceptionAsString = "\nTYPE: " + ex.GetType() +
                    "\nMESSAGE: " + ex.Message +
                    "\nSTACKTRACE: " + ex.StackTrace;

                Log(exceptionAsString);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves exception occurred to log and then shows exception message to user.
        /// </summary>
        /// <param name="ex">Exception to save information about</param>
        public static void LogAndNotify(Exception ex)
        {
            if (ex == null)
            {
                Debug.Assert(false, "Invalid argument at LogAndNotify");
                return;
            }

            try
            {
                string exceptionAsString = "\nTYPE: " + ex.GetType() +
                    "\nMESSAGE: " + ex.Message +
                    "\nSTACKTRACE: " + ex.StackTrace;

                Log(exceptionAsString);
                Messages.ShowError(ex.Message);
            }
            catch
            {
            }
        }

        #endregion

        #region Internal functionality

        /// <summary>
        /// Implements logic to save some data to log file. 
        /// If logging fails then it saves exception to event log (Application section).
        /// </summary>
        /// <param name="data">Data to save to log file.</param>
        /// <exception cref= "System.ArgumentNullException">Thrown when data is null or empty</exception>
        private static void DataToFile(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data", "Data can't be null or empty");
            }

            try
            {
                // make this logic thread safe
                lock (logFilePath)
                {
                    // obtain the virtual store 
                    IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

                    // write data
                    using (StreamWriter sw = new StreamWriter(new IsolatedStorageFileStream(logFilePath, FileMode.Append, myStore)))
                    {
                        sw.WriteLine(data);
                        sw.Close();
                    }
                }
            }
            catch (Exception)
            {
                Debug.Assert(false, "DataToFile unable save info to file");
            }
        }

        #endregion
    }
}
