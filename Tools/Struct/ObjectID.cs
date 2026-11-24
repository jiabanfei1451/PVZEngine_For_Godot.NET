using System;
using Godot;
using PVZEngine.Enum;
namespace PVZEngine.Type;
[GlobalClass]
public partial class ObjectID() : Resource
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
    public ObjectID HandleSaveID(Save.ObjectID SaveID)
    {
        ObjectID iD = new();
        iD.Object_Name_ID = SaveID.Object_Name_ID;
        iD.CH_Name = SaveID.CH_Name;
        iD.Object_ID = SaveID.Object_ID;
        iD.Index_Mode = SaveID.Index_Mode;
        return iD;
    }
}