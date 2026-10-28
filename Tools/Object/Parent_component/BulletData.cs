using System;
using Godot;

namespace PVZEngine.Object.Module;
/// <summary>
/// 射弹基础数据类型
/// </summary>
public partial class BulletData : ObjectPhysics
{
    /// <summary>
    /// 物体进入时
    /// </summary>
    /// <param name="node"></param>
    [Signal] public delegate void Object_JoinEventHandler(Node2D node);
    /// <summary>
    /// 物体离开时
    /// </summary>
    /// <param name="node"></param>
    [Signal] public delegate void Object_ExitEventHandler(Node2D node);
    /// <summary>
    /// 检测阵营
    /// </summary>
    [ExportGroup("Cheak_Group")]
    [Export] public Godot.Collections.Array<StringName> detection_Group = new Godot.Collections.Array<StringName>();
    /// <summary>
    /// 排除阵营
    /// </summary>
    [Export] public Godot.Collections.Array<StringName> Exclude_Group = new Godot.Collections.Array<StringName>(){"Bullet","Flight"}; 
    [ExportGroup("Array")]
    /// <summary>
    /// 已检测到的物体
    /// </summary>
    [Export] internal Godot.Collections.Array<LevelObject> Check_Object = new Godot.Collections.Array<LevelObject>();
    /// <summary>
    /// 伤害
    /// </summary>
    [ExportGroup("Variant")] [Export] public int Damage = 20;
    [Export] public VisibleOnScreenNotifier2D VisibleOnScreenEnabler = null;
    /// <summary>
    /// 攻击完成自动移除物体
    /// </summary>
    [Export] public bool Damage_End_AutoRemove_Object = false;
    /// <summary>
    /// 速度
    /// </summary>
    [ExportGroup("Vector")]
    [Export] public Godot.Vector2 Speed = new Vector2(1,1);
    public override void _Ready() {
        base._Ready();
        if (!(VisibleOnScreenEnabler is null))
        {
            VisibleOnScreenEnabler.ScreenExited += Exit_Free;
        }
    }
    void Exit_Free()
    {
        QueueFree();    
    }
    public Vector2 Get_Rotation_Vector(float Rotation = 0)
    {
        Vector2 Temp_Vector;
        Temp_Vector = Vector2.FromAngle(3.14f / 180 * Rotation);
        return Temp_Vector;
    }
    public float GEt_Rotation(Vector2 vector)
    {
        Vector2 temp_Postiion = Position - vector;
        return MathF.Atan2(temp_Postiion.Y,temp_Postiion.X); 
    }
    public void Damage_Object(int Array_Index)
    {
        Check_Object[Array_Index].Reduce_Health(10,this);
    }
    public void Add_Check_Object(LevelObject data_object)
    {
        if (Check_Object.IndexOf(data_object) != -1){return;}
        Check_Object.Add(data_object);
    }
    public void Remove_Check_Object(LevelObject data_object)
    {
        Check_Object.Remove(data_object);
    }
    internal void Join(Node2D node)
    {
        if (node is LevelObject){
            bool Cheak = Game.Cheak.CheakGroup.Cheak_Object_Group(node,detection_Group,Exclude_Group);
            if (!Cheak){return;}
            Add_Check_Object((LevelObject)node);
            EmitSignal("Object_Join",node);
        }
    }
    internal void Exit(Node2D node)
    {
        if (node is LevelObject)
        {
            Remove_Check_Object((LevelObject)node);
            EmitSignal("Object_Exit",node);
        }
    }
}
