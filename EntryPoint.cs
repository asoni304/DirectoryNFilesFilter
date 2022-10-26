using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

internal class EntryPoint
{
    static void Main()
    {
        //1.Get Main directory and sorted subdirectory
        string sortedPath = "sorted";
        string mainPath = @"C:\Users\User\Desktop\projects\c#\FilesAndDirectoryProject\bin\Debug\files";

        DirectoryInfo mainDirectory = new DirectoryInfo(mainPath);

        CreateSubDirectory(sortedPath, mainPath, mainDirectory);


        //2.Get files from main directory
        FileInfo[] files = mainDirectory.GetFiles();

        List<FileInfo> aviFiles = new List<FileInfo>();
        List<FileInfo> srtFiles = new List<FileInfo>();
        List<FileInfo> srtFilesToKeep = new List<FileInfo>();

        GetFiles(files, aviFiles, srtFiles);



        //3.Sort the files ,which we keep and those we don't

        SortFiles(aviFiles, srtFiles, srtFilesToKeep);



        //4.Move the files that we will keep in the sorted directory

        MoveToSorted(sortedPath, mainPath, aviFiles, srtFilesToKeep);

    }

    private static void GetFiles(FileInfo[] files, List<FileInfo> aviFiles, List<FileInfo> srtFiles)
    {
        foreach (var item in files)
        {
            if (item.Extension == ".avi")
            {
                aviFiles.Add(item);
            }
            else if (item.Extension == ".srt")
            {
                srtFiles.Add(item);
            }
        }
    }


    private static void SortFiles(List<FileInfo> aviFiles, List<FileInfo> srtFiles, List<FileInfo> srtFilesToKeep)
    {
        string aviName = string.Empty;
        string srtName = string.Empty;

        foreach (var avi in aviFiles)

        {
            aviName = avi.Name.Substring(0, avi.Name.LastIndexOf("."));
            foreach (var srt in srtFiles)
            {
                srtName = srt.Name.Substring(0, srt.Name.LastIndexOf("."));
                if (aviName == srtName)
                {
                    srtFilesToKeep.Add(srt);
                }
            }
        }
    }

    private static void MoveToSorted(string sortedPath, string mainPath, List<FileInfo> aviFiles, List<FileInfo> srtFilesToKeep)
    {
        for (int i = 0; i < aviFiles.Count; i++)
        {
            aviFiles[i].MoveTo(mainPath + "\\" + sortedPath + "\\" + aviFiles[i].Name);

            srtFilesToKeep[i].MoveTo(mainPath + "\\" + sortedPath + "\\" + srtFilesToKeep[i].Name);

            Thread.Sleep(500);

        }
    }

    private static void CreateSubDirectory(string sortedPath, string mainPath, DirectoryInfo mainDirectory)
    {
        if (!Directory.Exists(mainPath + "\\" + sortedPath))
        {
            mainDirectory.CreateSubdirectory(sortedPath);
        }
    }
}

