//-----------------------------------------------------------------------
// <copyright file="FileUtils.cs" company="Akvelon">
//     Copyright (c) Akvelon. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

using System;
using System.IO.IsolatedStorage;
using System.IO;

namespace Support
{
    public static class FileUtils
    {
        public static string ReadFileContent(string path)
        {
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(path, FileMode.Open, FileAccess.Read);
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
            {
                return reader.ReadToEnd();
            }

            return string.Empty;
        }

    }
}
