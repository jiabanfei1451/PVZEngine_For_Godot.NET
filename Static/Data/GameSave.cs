using System;
using System.Collections.Generic;
using Godot;
namespace PVZEngine.Static;
public static class GameSave
{
    /// <summary>
    /// 返回实例
    /// </summary>
    public static Action<object> ReturnObject;
    
    public static int SaveDataLength = 0;
    public static int EndSaveDataLength = 0;
    public static bool SaveIng = false;
    public static List<int> Queue = new();
    public static int Current_Queue = -1;    
    public static String[] PropertyName = new String[]{};
    public static object[] SaveProperty = new object[]{};

}