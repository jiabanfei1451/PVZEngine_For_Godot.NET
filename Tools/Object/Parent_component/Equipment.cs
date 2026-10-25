using Game;
using Godot;
using PVZEngine.Data;
using PVZEngine.Module;
using PVZEngine.Static;
namespace PVZEngine.Object.Module;
/// <summary>
/// 器械组件
/// </summary>
[Icon("uid://cs87xcexy6nl7")]
public partial class Equipment : LevelObject
{
    /// <summary>
    /// 叠加放置同类型物体时
    /// </summary>
    /// <param name="Placement_Object"></param>
    [Signal] public delegate void Stacked_PlacementEventHandler(LevelObject Placement_Object);
    /// <summary>
    /// 叠加放置自身时
    /// </summary>
    /// <param name="Placement_Object"></param>
    [Signal] public delegate void Stacked_PlacementThisEventHandler(LevelObject Placement_Object);
    /// <summary>
    /// 激活
    /// </summary>
    [Signal] public delegate void Activate_EquipmentEventHandler();
    /// <summary>
    /// 驱动器械
    /// </summary>
    [Signal] public delegate void Driver_EquipmentEventHandler();
    [ExportGroup("Object")]
    [Export] public Touch.TouchPad TouchPad = null;
    [ExportGroup("Number")]
    [Export] public int Kill_Number = 50;
    /// <summary>
    /// 颜色
    /// </summary>
    [Export] public int Particie_Number = 2;
    /// <summary>
    /// 颜色
    /// </summary>
    [Export] public Godot.Collections.Array<Color> Colors = new();
    /// <summary>
    /// 大招状态
    /// </summary>
    [Export] public bool Ultimate_Move = false;
    /// <summary>
    /// 死亡自动销毁
    /// </summary>
    [Export] public bool Kill_Auto_Free = true;
    /// <summary>
    /// 放置音效组
    /// </summary>
    [Export] public Godot.Collections.Array<AudioStream> PlacedAudios = new(){};
    /// <summary>
    /// 死亡音效组
    /// </summary>
    [Export] public Godot.Collections.Array<AudioStream> KillAudios = new(){};
    /// <summary>
    /// 已激活
    /// </summary>
    [Export] public bool Activate = false;
    /// <summary>
    /// 驱动所需
    /// </summary>
    [Export] public short Driver_required = 0;
    /// <summary>
    /// 可激活
    /// </summary>
    [Export] public bool Activatable = false;
    /// <summary>
    /// 可驱动
    /// </summary>
    [Export] public bool drivable = false;
    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (!Enable){return;}
        if (Level_Script.Use_Prop == Level_Script.Prop.Not){Modulate = new Color(1,1,1,1);return;}
        if (Is_Selected_This())
        {
            Modulate = new Color(2,2,2,1);
        }
        else
        {
            Modulate = new Color(1,1,1,1);
        }
    }
    public override void _ExitTree() {
        base._ExitTree();
        if (Level_Script.Selected_Object == this)
        {
            Level_Script.Selected_Object = null;
        }
    }
    public override void _Ready() {
        base._Ready();
        if (!Enable){return;}
        if (PlacedAudios.Count > 0)
        {
            Audio_Plus audio = new();
            audio.Audio_Type = Audio_Plus.Audio.Souds;
            audio.Auto_Get_File = false;
            audio.Auto_QueneFree = true;
            audio.Autoplay = true;
            audio.Stream = PlacedAudios.PickRandom();
            GetTree().CurrentScene.AddChild(audio);
            audio.Play();
            
        }
        Health_Reduce += This_Damage;
        Kill += This_Kill;
        if (TouchPad != null){
            TouchPad.Focus_Joinvoid += Selected_This;
            TouchPad.Focus_Exitvoid += Selected_Null;
            TouchPad.Button_Pressedvoid += Pressed;
        }
    }
    public void Selected_Null()
    {
        Level_Script.Selected_Object = null;
    }
    public void Selected_This()
    {
        Level_Script.Selected_Object = this;
    }
    /// <summary>
    /// 受击释放粒子
    /// </summary>
    /// <param name="Damage_Object"></param>
    public void This_Damage(Node Damage_Object)
    {
        for (int i = 0;i < Particie_Number;i++){
            float x = -PVZEngine.Module.Random.NextFloat_32(24,0);
            float y = -PVZEngine.Module.Random.NextFloat_32(21,0);
            float x2 = PVZEngine.Module.Random.NextFloat_32(24,0);
            float y2 = PVZEngine.Module.Random.NextFloat_32(22,0);
            Vector2 Summand_Position = new(x + x2,y + y2);
            PackedScene packed = ResourceScene.LoadScene("res://Object/Effect/Particle/Default.tscn");
            MVZ2.Object.Particie Summand_Particie = packed.Instantiate<MVZ2.Object.Particie>();
            Summand_Particie.Enable = false;
            NodeData.Get_Node<Node2D>("particle").AddChild(Summand_Particie);
            Summand_Particie.Position = Position + Summand_Position;
            Summand_Particie.Random_Color = false;
            Summand_Particie.Modulate = Colors.PickRandom();
            Summand_Particie.EmitSignal(MVZ2.Object.Particie.SignalName.Initialization);
        }
    }
    public void Pressed()
    {
        if (!Enable){return;}
        if (Level_Script.Use_Prop == Level_Script.Prop.iron_pickaxe)
        {
            if (Is_Selected_This() == true)
            {
                Level_Script.Use_Prop = Level_Script.Prop.Not;
                This_Kill();
            }
        }
        if (Level_Script.Use_Prop == Level_Script.Prop.Star_Fragment)
        {
            if (Is_Selected_This() == true)
            {
                if (Level_Script.Star_Fragment > 0)
                {
                    GD.Print(Level_Script.Star_Fragment);
                    Level_Script.Star_Fragment -= 1;
                    Level_Script.Use_Prop = Level_Script.Prop.Not;
                    Audio_Plus audio = new();
                    audio.Audio_Type = Audio_Plus.Audio.Souds;
                    audio.fade_Time = 0;
                    audio.Audio_ID = "CH:激发器械";
                    audio.Auto_QueneFree = true;
                    audio.Auto_Get_File = false;
                    audio.Stream = PVZEngine.Static.Get_GlobalNode.Get_Audio_List(GetTree()).Get_Souds("CH:激发器械");
                    audio.Autoplay = true;
                    GetTree().CurrentScene.AddChild(audio);
                    audio.Play();
                    EmitSignalActivate_Equipment();
                }
            }
        }
        if (Level_Script.Use_Prop == Level_Script.Prop.Equipment_capable)
        {
            if (Is_Selected_This() == true)
            {
                if (Level_Script.Equipment_Capable >= Driver_required)
                {
                    Level_Script.Equipment_Capable -= Driver_required;
                    Level_Script.Use_Prop = Level_Script.Prop.Not;
                    EmitSignalDriver_Equipment();
                }
            }
        }
    }
    /// <summary>
    /// 死亡生成粒子
    /// </summary>
    public void This_Kill()
    {
        if (!Kill_Auto_Free){return;}
        if (!Enable){return;}
        if (KillAudios.Count > 0)
        {
            Audio_Plus audio = new();
            audio.Audio_Type = Audio_Plus.Audio.Souds;
            audio.Auto_Get_File = false;
            audio.Auto_QueneFree = true;
            audio.Autoplay = true;
            audio.Stream = KillAudios.PickRandom();
            GetTree().CurrentScene.AddChild(audio);
            audio.Play();
        }
        if (level != null){
            level.EmitSignal("Object_Kill",this);
        }
        Modulate = new Color(0,0,0,0);
        for (int i = 0;i < Kill_Number;i++){
            float x = -PVZEngine.Module.Random.NextFloat_32(24,0);
            float y = -PVZEngine.Module.Random.NextFloat_32(21,0);
            float x2 = PVZEngine.Module.Random.NextFloat_32(24,0);
            float y2 = PVZEngine.Module.Random.NextFloat_32(22,0);
            Vector2 Summand_Position = new(x + x2,y + y2);
            PackedScene packed = ResourceScene.LoadScene("res://Object/Effect/Particle/Default.tscn");
            MVZ2.Object.Particie Summand_Particie = packed.Instantiate<MVZ2.Object.Particie>();
            Summand_Particie.Enable = false;
            NodeData.Get_Node<Node2D>("particle").AddChild(Summand_Particie);
            Summand_Particie.Position = Position + Summand_Position;
            Summand_Particie.Random_Color = false;
            Summand_Particie.Modulate = Colors.PickRandom();
            Summand_Particie.EmitSignal(MVZ2.Object.Particie.SignalName.Initialization);
        }
        QueueFree();
    }
    public bool Is_Selected_This()
    {
        if (Level_Script.Selected_Object == this)
        {
            if (Level_Script.Use_Prop == Level_Script.Prop.Star_Fragment && Activatable && !Activate)
            {
                return true;
            }
            if (Level_Script.Use_Prop == Level_Script.Prop.Equipment_capable && drivable)
            {
                return true;
            }if (Level_Script.Use_Prop == Level_Script.Prop.iron_pickaxe)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
