using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfReplaceTest
{
    public class Util
    {
        public static void DirectoryCopyWithRenameFile(string sourceDirName, string destDirName,
                                      bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }


            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                for (int i = 0; i < 100; i++)
                {
                    string backupName = temppath + DateTime.Now.ToString("yyMMdd") + i.ToString();
                    if (File.Exists(backupName))
                    {
                        continue;
                    }
                    else
                    {
                        File.Move(temppath, backupName);
                        break;
                    }
                }
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopyWithRenameFile(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
