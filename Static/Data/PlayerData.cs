using PVZEngine.UI;
using Godot;
using System;
using System.Collections.Generic;
using PVZEngine.Enum;
using PVZEngine.Object.Module;

namespace PVZEngine.Data;
public static class PlayerData : System.Object
{
    public static Data Savedata = new Data();
    /// <summary>
    /// 由JSON存储的文本数据
    /// </summary>
    public static Godot.Collections.Array<Godot.Collections.Array> LoadJsonData = new Godot.Collections.Array<Godot.Collections.Array>()
    {
        /// NBTID
        new Godot.Collections.Array(){"CH:Name","CH:Level","EN:Name","EN:Level"},
        // JSONResource
        new Godot.Collections.Array(){
            ResourceLoader.Load("res://2/Text/Zh_CN/Level_Name.json"),
            ResourceLoader.Load("res://2/Text/Zh_CN/Level_Text.json"),
            ResourceLoader.Load("res://2/Text/EN_US/Level_Name.json"),
            ResourceLoader.Load("res://2/Text/EN_US/Level_Text.json")},
    };
    /// <summary>
    /// 临时器械数据
    /// </summary>
    public static LevelObject Temp_CardObject = null;
    /// <summary>
    /// 临时卡槽数据
    /// </summary>
    public static Card Temp_Card = null;
    public class Data()
    {
        /// <summary>
        /// 名称
        /// </summary>
        public String Name {get;set;} = "痊愈加班费";
        /// <summary>
        /// 最大卡槽数量 实际值是当前值 + 1
        /// </summary>
        public int Card_Quantity {get;set;} = 5;
        /// <summary>
        /// 最大星之碎片
        /// </summary>
        public byte Max_Star_Fragment {get;set;} = 3;
        /// <summary>
        /// 难度
        /// </summary>
        public Difficult Difficult {get;set;} = Enum.Difficult.Normal;
        /// <summary>
        /// 版本号
        /// </summary>
        public String Version {get;set;} = "Bata0.1.6";
        /// <summary>
        /// 版本等级
        /// </summary>
        public int VersionLevel {get;set;} = 1;
        /// <summary>
        /// 游戏加速
        /// </summary>
        public byte AddSpeed {get;set;} = 2;
    }
}