using System;
using System.IO;
using System.Text;
using Godot;

namespace PVZEngine.Static;
public static class FileSystem
{
    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="DirPath"></param>
    public static void CreateDir(String DirPath)
    {
        String Godot_Path = ProjectSettings.GlobalizePath(DirPath);
        if (Directory.Exists(Godot_Path) == false)
        {
            Directory.CreateDirectory(Godot_Path);
        }
    }
    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="DirPath"></param>
    public static void deleteDir(String DirPath)
    {
        String Godot_Path = ProjectSettings.GlobalizePath(DirPath);
        if (Directory.Exists(Godot_Path) == true)
        {
            Directory.Delete(Godot_Path);
        }
    }
    /// <summary>
    /// 打开文件，带识别
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    public static FileStream OpenFile(String FilePath)
    {
        String Godot_Path = ProjectSettings.GlobalizePath(FilePath);
        FileStream stream = File.Open(Godot_Path, FileMode.OpenOrCreate);
        return stream;
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="FilePath"></param>
    public static void DeleteFile(String FilePath)
    {
        String Godot_Path = ProjectSettings.GlobalizePath(FilePath);
        if (File.Exists(Godot_Path))
        {
            File.Delete(FilePath);
        }
    }
    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    public static FileStream CreateFile(String FilePath)
    {
        String Godot_Path = ProjectSettings.GlobalizePath(FilePath);
        FileStream stream = File.Create(Godot_Path);
        return stream;
    }
    /// <summary>
    /// 处理JSON文本
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static String Handle_JsonText(String Text)
    {
        String Temp_Text = "";
        int TabNumber = 0;
        bool StartNumber = true;
        foreach(char Str in Text)
        {
            Temp_Text += Str;
            switch (Str)
            {
                case '(':
                    StartNumber = false;
                    break;
                case ')':
                    StartNumber = true;
                    break;
                case ',':
                    if (!StartNumber)
                    {
                        break;
                    }
                    Temp_Text += "\n";
                    for(int number = 0;number < TabNumber; number++)
                    {
                        Temp_Text += @"  ";
                    }
                    break;
                case '{':
                    TabNumber += 1;
                    Temp_Text += "\n";
                    for(int number = 0;number < TabNumber; number++)
                    {
                        Temp_Text += @"  ";
                    }
                    break;
                case '}':
                    TabNumber -= 1;
                    Temp_Text = Temp_Text[0..(Temp_Text.Length - 1)];
                    Temp_Text += "\n";
                    for(int number = 0;number < TabNumber; number++)
                    {
                        Temp_Text += @"  ";
                    }
                    Temp_Text += "}";
                    break;
            }
        }
        return Temp_Text;
    }
    /// <summary>
    /// 写入文件,并关闭
    /// </summary>
    /// <param name="FilePath"></param>
    public static void WriteFileandoff(FileStream fileStream,String WriteText)
    {
        byte[] WriteByte = Encoding.UTF8.GetBytes(WriteText);
        fileStream.Write(WriteByte);
        fileStream.Close();
    }
    public static String Get_FileText(String FilePath)
    {
        String Godot_Path = ProjectSettings.GlobalizePath(FilePath);
        String Get_FileText = File.ReadAllText(Godot_Path);
        return Get_FileText;
    }
    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="FilePath"></param>
    public static void WriteFile(FileStream fileStream,String WriteText)
    {
        byte[] WriteByte = Encoding.UTF8.GetBytes(WriteText);
        fileStream.Write(WriteByte);
    }
}