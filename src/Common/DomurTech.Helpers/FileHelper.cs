using System;
using System.IO;

namespace DomurTech.Helpers
{
    public static class FileHelper
    {
        public static string ReadAllLines(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new Exception("Stream did not contain properly formatted byte array");
            }
            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new Exception("Did not read byte array properly");
            }
            return buffer;
        }

        public static void CopyDirectory(string sourceFolderPath, string targetFolderPath)
        {

            if (targetFolderPath[targetFolderPath.Length - 1] != Path.DirectorySeparatorChar)
            {
                targetFolderPath += Path.DirectorySeparatorChar;
            }

            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }

            var files = Directory.GetFileSystemEntries(sourceFolderPath);
            foreach (var file in files)
            {

                if (Directory.Exists(file))
                {
                    CopyDirectory(file, targetFolderPath + Path.GetFileName(file));
                }
                else
                {
                    File.Copy(file, targetFolderPath + Path.GetFileName(file), true);
                }
            }


        }
    }
}
