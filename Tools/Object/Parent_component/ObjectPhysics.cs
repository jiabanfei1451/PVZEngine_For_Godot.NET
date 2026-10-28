using Godot;
using System;
using PVZEngine.Enum;
using PVZEngine.Static;
using System.Text;
namespace PVZEngine.Object.Module;
[Icon("uid://dwrrtr5iyf6yq")]
public partial class ObjectPhysics : Node2D
{
    /// <summary>
    /// 当前草坪行数索引
    /// </summary>
    [ExportGroup("Index")]
    [Export] public int Lawn_Index = -1;
    /// <summary>
    /// 检测范围
    /// </summary>
    [Export] public Godot.Vector2I Cheak_Index_Scoop = new(0,0);
    /// <summary>
    /// 可被其他物体跨行检测的范围
    /// </summary>
    [Export] public Godot.Vector2I Cheak_Lawn_Index_Scoop = new(0,0);
    /// <summary>
    /// 自动设置草坪行数索引
    /// </summary>
    [Export] public bool AutoSet_Lawn_Index = true;
    /// <summary>
    /// 检测
    /// </summary>
    /// <summary>
    /// 可检测高度等级
    /// </summary>
    [ExportGroup("Cheak")]
    [Export] public Godot.Vector2 Cheak_Height_Level_Scoop = new Vector2(-1,1);
    /// <summary>
    /// 当前高度等级
    /// </summary>
    [Export] public float Height_Level = 2;
    /// <summary>
    /// 速度
    /// </summary>
    [ExportGroup("Vector")]
    [Export] public Godot.Vector2 position_Offset = Godot.Vector2.Zero;
    /// <summary>
    /// 启用物理
    /// </summary>
    [ExportGroup("Physics")]
    [Export] public bool Physics_Enable = true;
    /// <summary>
    /// 重量
    /// </summary>发`
    [Export] public float Weight = 5;
    /// <summary>
    /// 坐标类型
    /// </summary>
    [Export] public PositionType PositionType = PositionType.GlobalPosition;
    /// <summary>
    /// 跟随当前重量增加加速度
    /// </summary>
    [Export] public bool Follow_the_current_weight = false;
    /// <summary>
    /// 下落加速度
    /// </summary>
    [Export] public float Falling_Acceleration = 1;
    /// <summary>
    /// 检测高度偏移（用于校准）
    /// </summary>
    [Export] public float Cheak_Height_Offset = 0;
    /// <summary>
    /// 高度
    /// </summary>
    [Export] public float Height = 0;
    /// <summary>
    /// 最低高度
    /// </summary>
    [Export] public float min_Height = 0;
    /// <summary>
    /// 额外高度
    /// </summary>
    [Export] public float Extra_Height = 0;
    /// <summary>
    /// 可检测高度
    /// </summary>
    [Export] public Godot.Vector2 Cheak_Height_Scoop = new(-5,15);
    /// <summary>
    /// 实际坐标
    /// </summary>
    [Export] public Godot.Vector2 practical_Position = new Godot.Vector2();
    /// <summary>
    /// 加速度
    /// </summary>
    [Export] public float Acceleration = 10;
    /// <summary>
    /// 物理已完成初始化
    /// </summary>
    [Export] internal bool Physics_Initialization = false;
    /// <summary>
    /// 阴影场景
    /// </summary>
    [ExportGroup("Shadow")][Export] public PackedScene ShadowScene = PVZEngine.ResourceScene.LoadScene("uid://2sgut6k4shoy");
    /// <summary>
    /// 阴影
    /// </summary>
    [Export] public Node2D Shadow = null;
    /// <summary>
    /// 阴影尺寸乘积
    /// </summary>
    [Export] public Godot.Vector2 Shadow_SizeMultiplication = new Vector2(1,1);
    /// <summary>
    /// 阴影最小尺寸
    /// </summary>
    [Export] public Godot.Vector2 MinShadow_Size = Vector2.Zero;
    /// <summary>
    /// 阴影尺寸
    /// </summary>
    [Export] public Godot.Vector2 Shadow_Size = new(1,1);
    /// <summary>
    /// 阴影偏移
    /// </summary>
    [Export] public Godot.Vector2 Shadow_Offset = Vector2.Zero;
    /// <summary>
    /// 临时坐标
    /// </summary>
    float Temp_Position_Y = -1;
    internal Level_Master_Script level {get;set;} = null;
    /// <summary>
    /// 标签
    /// </summary>
    [ExportGroup("Misc")]
    [Export] public Godot.Collections.Array<String> Tags = new(){};
    /// <summary>
    /// 物体类型
    /// </summary>
    [Export] public ObjectType Object_Type = ObjectType.Normal;
    /// <summary>
    /// 对象内部ID
    /// </summary>
    [Export] public String Object_UUID = "MVZ2:";
    /// <summary>
    /// 对象名称
    /// </summary>
    [Export] public String Object_Name = "";
    /// <summary>
    /// 对象ID
    /// </summary>
    [Export] public int Object_ID = -1;
    /// <summary>
    /// 搜索索引模式
    /// </summary>
    [Export] public IndexMode Index_Mode = IndexMode.Name;
    /// <summary>
    /// 启用放置依赖
    /// </summary>
    [ExportGroup("Placed")]
    [Export] public bool Enable_Placed_Reliant = false;
    /// <summary>
    /// 自动释放
    /// </summary>
    [Export] public bool Auto_Free_Reliant = true;
    /// <summary>
    /// 依赖物体UUID
    /// </summary>
    [Export] public Godot.Collections.Array<String> Reliant_UUID = new();
    /// <summary>
    /// 依赖标签
    /// </summary>
    [Export] public Godot.Collections.Array<String> Reliant_Tag = new();
    /// <summary>
    /// 不可放置类型
    /// </summary>
    [Export] public Godot.Collections.Array<ObjectType> Cannot_place_Type = new(){};
    /// <summary>
    /// 可叠加放置
    /// </summary>
    [Export] public bool Stackable = false;
    /// <summary>
    /// 默认生成层
    /// </summary>
    [Export] public String Default_generation_layer;
    /// <summary>
    /// 叠加放置UUID白名单
    /// </summary>
    [Export] public Godot.Collections.Array<String> Stackable_UUID = new(){};
    /// <summary>
    /// 重置物理坐标
    /// </summary>
    public override void _ExitTree() {
        base._ExitTree();
        if (!Cheak.is_null<Node2D>(Shadow))
        {
            Shadow.QueueFree();
        }
    }
    internal void Reset_Position()
    {
        if(PositionType == PositionType.GlobalPosition){
            practical_Position = GlobalPosition;
            Physics_Initialization = true;
        }
        else
        {
            practical_Position = Position;
        }
    }
    /// <summary>
    /// 设置物理坐标
    /// </summary>
    /// <param name="delta"></param>
    public void SetPhysics_Position(double delta) {
        try{
            float FloatDelta = (float)delta;
            if (!Physics_Enable){return;}
            if (!Physics_Initialization){return;}
            if (Cheak.is_null<Node2D>(Shadow))
            {
                Shadow = ShadowScene.Instantiate<Node2D>();
                Shadow.Visible = false;
                float Shadow_Scale_X = 0.2f * Shadow_Size.X;
                float Shadow_Scale_Y = 0.125f * Shadow_Size.Y;
                
                if (Height / (20 / Shadow_SizeMultiplication.X) > 1)
                {
                    Shadow_Scale_X /= Height / (20 / Shadow_SizeMultiplication.X);
                }
                if (Height / (20 / Shadow_SizeMultiplication.Y) > 1)
                {
                    Shadow_Scale_Y /= Height / (20 / Shadow_SizeMultiplication.Y);
                }
                NodeData.Get_Node<CanvasGroup>("Shadow").AddChild(Shadow);
            }
            else
            {
                Shadow.Visible = true;
                float Shadow_Scale_X = 0.2f * Shadow_Size.X;
                float Shadow_Scale_Y = 0.125f * Shadow_Size.Y;
                if (Height / (20 / Shadow_SizeMultiplication.X) > 1)
                {
                    Shadow_Scale_X /= Height / (20 / Shadow_SizeMultiplication.X);
                }
                if (Height / (20 / Shadow_SizeMultiplication.Y) > 1)
                {
                    Shadow_Scale_Y /= Height / (20 / Shadow_SizeMultiplication.Y);
                }
                Shadow.Scale = new(Shadow_Scale_X,Shadow_Scale_Y);
                if (PositionType == PositionType.localPosition)
                {
                    Shadow.Position = practical_Position + Shadow_Offset;
                }
                else
                {
                    Shadow.GlobalPosition = practical_Position + Shadow_Offset;
                }
            }
            if (Height > min_Height)
            {
                if (Follow_the_current_weight == true){
                    Falling_Acceleration += Weight * Acceleration * FloatDelta;
                }
                else
                {
                    Falling_Acceleration += Acceleration * FloatDelta;
                }
                Height -= Falling_Acceleration;
            }

            if(Height < min_Height)
            {
                Falling_Acceleration = 0;
                Height = min_Height;
            }
            if (PositionType == PositionType.GlobalPosition){
                GlobalPosition = practical_Position - new Vector2(0,Height + Extra_Height);
            }
            else
            {
                Position = practical_Position - new Vector2(0,Height + Extra_Height);
            }
        }catch(Exception EX)
        {
            DEBUG.Info.Print(EX);
        }
    }
    /// <summary>
    /// 检测高度等级
    /// <para>
    /// 检测高度等级值 + 当前高度值 
    /// </para>
    /// </summary>
    /// <param name="physics"></param>
    /// <returns></returns>
    public bool Cheak_HeightLevel(ObjectPhysics physics)
    {
        if (Cheak.is_null<ObjectPhysics>(physics)){return false;}
        float Extra_Height_Level = 0;
        float Min_Heigit = Cheak_Height_Scoop.X + Height + Extra_Height;
        float Max_Height = Cheak_Height_Scoop.Y + Height + Extra_Height;
        if (Min_Heigit >= Max_Height)
        {
            float Temp = Min_Heigit;
            Min_Heigit = Max_Height;
            Max_Height = Temp;
        }
        if ((physics.Height + physics.Extra_Height) > Max_Height)
        {
            Extra_Height_Level = (physics.Height + physics.Extra_Height + physics.Cheak_Height_Offset) / Max_Height;
        }
        if ((physics.Height + physics.Extra_Height) < Min_Heigit)
        {
            Extra_Height_Level -= (physics.Height + physics.Extra_Height + physics.Cheak_Height_Offset) / Max_Height;
        }
        float Max_Height_Level = Cheak_Height_Level_Scoop.Y + Height_Level;
        float Min_Height_Level = Cheak_Height_Level_Scoop.X + Height_Level;
        if (Min_Height_Level >= Max_Height_Level)
        {
            float temp = Min_Height_Level;
            Min_Height_Level = Max_Height_Level;
            Max_Height_Level = temp;
        }
        float Current_Height = physics.Height_Level + Extra_Height_Level;
        if (Current_Height <= Max_Height_Level && Current_Height >= Min_Height_Level)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 检测高度是否可以判定
    /// </summary>
    /// <param name="physics">引用Level.Module.ObjectPhysics的物体</param>
    /// <returns></returns>
    public bool Cheak_Height(ObjectPhysics physics)
    {
        if (Cheak.is_null<ObjectPhysics>(physics)){return false;}
        float Min_Heigit = Cheak_Height_Scoop.X + Height + Extra_Height;
        float Max_Height = Cheak_Height_Scoop.Y + Height + Extra_Height;
        float Current_Height = physics.Height + physics.Cheak_Height_Offset + physics.Extra_Height;
        if (Min_Heigit >= Max_Height)
        {
            float Temp = Min_Heigit;
            Min_Heigit = Max_Height;
            Max_Height = Temp;
        }
        if (Current_Height <= Max_Height && Current_Height >= Min_Heigit)
        {
            return true;
        }
        return false;

    }
    /// <summary>
    /// 检测草坪行
    /// </summary>
    /// <param name="CheakObject"></param>
    /// <returns></returns>
    public bool Cheak_Lawn_Index(ObjectPhysics CheakObject)
    {
        if (Cheak.is_null<ObjectPhysics>(CheakObject)){return false;}
        int Object_ScoopX = CheakObject.Cheak_Index_Scoop.X;
        int Object_ScoopY = CheakObject.Cheak_Index_Scoop.Y;
        if (Object_ScoopY > Object_ScoopX)
        {
            int Temp = Object_ScoopY;
            Object_ScoopY = Object_ScoopX;
            Object_ScoopX = Temp;
        }
        int Min_Cheak_Index = Cheak_Lawn_Index_Scoop.X + Object_ScoopY + Lawn_Index;
        int Max_Cheak_Index = Cheak_Lawn_Index_Scoop.Y + Object_ScoopX + Lawn_Index;
        int CheakObject_Index = CheakObject.Lawn_Index;
        if (CheakObject_Index <= Max_Cheak_Index && CheakObject_Index >= Min_Cheak_Index)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 重新设置索引
    /// </summary>
    public void reset_Lawn_Index()
    {
        if (Lawn_Index != -1){
            level.Move_Lawn_Index(this,level.Get_LawnIndex(Position,position_Offset));
        }
        else
        {
            Lawn_Index = level.Get_LawnIndex(Position,position_Offset);
            level.Add_Lawn_Index(this,Lawn_Index);
        }
    }
    public void Get_Level()
    {
        if (Cheak.is_null<Level_Master_Script>(level))
        {
            Level_Master_Script Get_Level = PVZEngine.Static.NodeData.Get_Node<Level_Master_Script>("Level");
            if (Get_Level != null)
            {
                level = Get_Level;
            }
        }
    }
    /// <summary>
    /// 草坪索引重定向
    /// </summary>
    public void ReSet_Index()
    {
        if (Cheak.is_null<Level_Master_Script>(level)){return;}
        // 高度重定向
        if (!Cheak.is_null<Level_Master_Script>(level) && AutoSet_Lawn_Index == true)
        {
            if (Temp_Position_Y != practical_Position.Y + position_Offset.Y)
            {
                Temp_Position_Y = practical_Position.Y + position_Offset.Y;
                level.EmitSignal(Level_Master_Script.SignalName.Object_Change_Height,this);
                reset_Lawn_Index();
            }
        }
    }
}
