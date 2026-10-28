using PVZEngine.Enum;
using Godot;
using System;

namespace PVZEngine.Data;

/// <summary>
/// 返回数据
/// </summary>
/// <param name="Scene">场景</param>
/// <param name="sonsume">消耗</param>
/// <param name="CD">冷却</param>
/// <param name="First_Time_RemoveCD">游戏开始时减少冷却</param>
/// <param name="Scale">大小</param>
/// <param name="Offset">偏移</param>
/// <param name="Mouse_Offset">光标坐标偏移</param>
/// <param name="Map_Offset">地图坐标偏移</param>
/// <param name="Map_Scale">地图大小</param>
public struct GlobalData(){

    /// <summary>
    /// 物体场景
    /// </summary>
    public PackedScene Scene {get;set;}
    /// <summary>
    /// 消耗
    /// </summary>
    public short Sonsume {get;set;}
    /// <summary>
    /// 冷却时间
    /// </summary>
    public float CD {get;set;}
    /// <summary>
    /// 首次冷却时减少冷却
    /// </summary>
    public float First_Time_RemoveCD {get;set;}
    /// <summary>
    /// 材质大小
    /// </summary>
    public Godot.Vector2 Scale {get;set;}
    /// <summary>
    /// 材质偏移
    /// </summary>
    public Godot.Vector2 Offset {get;set;}
    /// <summary>
    /// 移动端材质大小
    /// </summary>
    public Godot.Vector2 Pe_Scale {get;set;}
    /// <summary>
    /// 移动端偏移
    /// </summary>
    public Godot.Vector2 Pe_Offset {get;set;}
    /// <summary>
    /// 鼠标偏移
    /// </summary>
    public Godot.Vector2 Mouse_Offset {get;set;}
    /// <summary>
    /// 地图偏移
    /// </summary>
    public Godot.Vector2 Map_Offset {get;set;}
    /// <summary>
    /// 地图缩放
    /// </summary>
    public Godot.Vector2 Map_Scale {get;set;}
    /// <summary>
    /// 物体类型
    /// </summary>
    public ObjectType Object_Type {get;set;} = ObjectType.Normal;
    /// <summary>
    /// 这是事件卡
    /// </summary>
    public bool is_EventCard {get;set;} = false;
    /// <summary>
    /// 事件卡标签
    /// </summary>
    public String EventTag {get;set;} = "";
    /// <summary>
    /// 这是空值
    /// </summary>
    public bool is_Null = false;
    public static GlobalData Zero()
    {
        var s = new GlobalData();
        s.is_Null = true;
        return s;
    }
    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    public static ObjectType GetObjectType(int Index)
    {
        ObjectType[] types = new ObjectType[]{
        ObjectType.Null,
        ObjectType.Normal,
        ObjectType.Bearing_unit,
        ObjectType.Monster};
        if (Index < types.Length)
        {
            return types[Index];
        }
        else
        {
            return types[types.Length - 1];
        }
    }
}