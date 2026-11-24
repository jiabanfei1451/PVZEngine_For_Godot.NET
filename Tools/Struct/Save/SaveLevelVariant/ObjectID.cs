using System;
using Godot;
using PVZEngine.Enum;
namespace PVZEngine.Save;
public partial class ObjectID()
{
    /// <summary>
    /// 对象名称
    /// </summary>
    [Export] public String Object_Name_ID {get;set;}
    /// <summary>
    /// 中文名称
    /// </summary>
    [Export] public String CH_Name {get;set;}
    /// <summary>
    /// 物体ID
    /// </summary>
    [Export] public int Object_ID {get;set;}
    [Export] public String PackedScenePath = "res://";
    [Export] public IndexMode Index_Mode {get;set;}  = IndexMode.index;
}