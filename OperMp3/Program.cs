using Shell32;
using System;
using System.IO;

namespace OperMp3
{
    class Program
    {
        static void Main(string[] args)
        {
            //目录名
            string direName = "D:/经典老歌/";

            //找到目录
            DirectoryInfo directoryinfo = new DirectoryInfo(direName);

            //读取当前目录文件信息
            foreach (var item in directoryinfo.GetFiles())
            {
                //获得完整文件名
                string fileName = direName + item.Name;

                //得到歌曲文件对应的标题
                string musicTitle = getMusicName(fileName,21);
                Console.WriteLine("{0}", musicTitle);

                //重命名文件
                renameFile(direName, item.Name, musicTitle + ".mp3");            
            }                       
        }

        //得到歌曲标题
        //需要 reference shell32，并using Shell32，DLL的属性Embed Interop Type 设为False
        static string getMusicName(string file, int iCol)
        {
            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName(file));
            FolderItem item = dir.ParseName(Path.GetFileName(file));

            string str = dir.GetDetailsOf(item, iCol);

            return str;               
        }

        //MoveTo到原目录里一个新的名字实现重命名
        static void renameFile(string dirName, string oldName, string newName)
        {
            FileInfo fi = new FileInfo(dirName + oldName);
            fi.MoveTo(Path.Combine(dirName + newName));
        }
        
    }
}
