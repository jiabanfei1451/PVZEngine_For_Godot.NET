using Godot;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PVZEngine.Data;
using PVZEngine.Module;
using PVZEngine.Enum;
using PVZEngine.Type;
using PVZEngine.Object.Module;

namespace PVZEngine.Static;
/// <summary>
/// 生成组件
/// </summary>
public static class Summand
{
    /// <summary>
    /// 生成数据
    /// </summary>
    public static Summand_Data Data = null;
    /// <summary>
    /// 循环模式
    /// </summary>
    public static WhileMode whileMode = WhileMode._While;
    /// <summary>
    /// 循环中
    /// </summary>
    public static bool While_Ing = true;
    /// <summary>
    /// 生成物体
    /// </summary>
    /// <param name="Number"></param>
    /// <param name="Summand_OffsetIndex">指定的索引行当值为负值则随机</param>
    public static async void Summand_Object(int Number,int Summand_OffsetIndex = -1,int Array_Index = 0)
    {
        // 条件判断
        // 生成坐标数据数组检测
        if (Data.Summand_Position.Length <= 0 && Data.Summand_Position.Length <= 0){return;}
        if (Summand_OffsetIndex >= Data.Summand_Position.Length)
        {
            Summand_OffsetIndex = Data.Summand_Position.Length - 1;
        }
        if (Cheak.is_null<Summand_Data>(Summand.Data)){return;}
        if (Cheak.is_null<List<ObjectID>>(Summand.Data.Summand_List)){return;}
        if (Data.Summand_List.Count <= 0){return;}
        if (Cheak.is_null<Level_Master_Script>(Level_Script.Level_Object)){return;}
        // 获取生成坐标
        Godot.Vector2 Summand_Position = Level_Script.Level_Object.LawnSummandData[Array_Index].Lawn_Spawn_Position;
        Godot.Vector2 Summand_Offset = Level_Script.Level_Object.LawnSummandData[Array_Index].Lawn_Spawn_Offect;
        System.Random Ran = new System.Random();
        set_Timer();
        if (Data.Summand_ing == false)
        {
            Data.Max_Summand_Number = Number;
            Data.Current_Summand_Number = 0;
            Data.Summand_ing = true;
            while (Data.Current_Summand_Number < Data.Max_Summand_Number)
            {
                if (Data.Present_Object.Count < 50)
                {
                    int Temp_Count = Data.Summand_List.Count - 1;
                    ObjectID Random_Get = null;
                    Random_Get = Data.Summand_List[Ran.Next(0,Temp_Count)]; 
                    PackedScene GetPacked = null;
                    if (!(Random_Get is null)){
                        GetPacked = Object_List.Get_Packed(Random_Get);
                    }
                    if (!(GetPacked is null)){
                        GlobalData get_carddata = Card_Data.Get_CardData(GetPacked);
                        Godot.Vector2 Summand_Scale = new(1,1);
                        Godot.Vector2 Summanc_Offset = new(40,48);
                        if (Summand_OffsetIndex < 0)
                        {
                            Summand_OffsetIndex = Data.random.RandiRange(0,Data.Summand_Position.Length - 1);
                        }
                        if (get_carddata.is_Null == false)
                        {
                            Summand_Scale = get_carddata.Map_Scale;
                            Summanc_Offset = get_carddata.Map_Offset;
                        }
                        // 计算生成后坐标
                        DEBUG.Info.Print(Summand_OffsetIndex);
                        PVZEngine.Data.Vector2 GetSummand_Position = Data.Summand_Position[Summand_OffsetIndex];
                        PVZEngine.Data.Vector2i GetSummand_YOffset = Data.YPosition_Offset[Summand_OffsetIndex];
                        Godot.Vector2 Object_Summand_Offset = new Godot.Vector2(0,Data.random.RandiRange(GetSummand_YOffset.X,GetSummand_YOffset.Y) * Summand_Offset.Y);
                        Godot.Vector2 Data_Summand_Position = new Godot.Vector2(GetSummand_Position.X,GetSummand_Position.Y);
                        // 求和
                        Godot.Vector2 Object_Summand_Position = Summand_Position + Data_Summand_Position + Object_Summand_Offset + Summanc_Offset;
                        // 开始实例化
                        LevelObject Instantiate = get_carddata.Scene.InstantiateOrNull<LevelObject>();
                        // 生成类型不为Null时检测,如果生成物体不满足则重新选择
                        if (Data.Summand_ObjectType[Summand_OffsetIndex] != ObjectType.Null)
                        {
                            if (Instantiate.Object_Type != Data.Summand_ObjectType[Summand_OffsetIndex])
                            {
                                Instantiate.Free();
                                Instantiate = null;
                                Summand_OffsetIndex = Data.random.RandiRange(0,Data.Summand_Position.Length - 1);
                            }
                        }
                        if (!Cheak.is_null<LevelObject>(Instantiate))
                        {
                            Node2D Get_Node = null;
                            String layer = Instantiate.Default_generation_layer;
                            if (layer != "")
                            {
                                Get_Node = NodeData.Get_Node<Node2D>(Instantiate.Default_generation_layer);
                            }
                            if (Cheak.is_null<Node2D>(Get_Node))
                            {
                                if (Instantiate.IsInGroup("Monster"))
                                {
                                    Get_Node = NodeData.Get_Node<Node2D>("Monster");
                                }
                                else
                                {
                                    Get_Node = NodeData.Get_Node<Node2D>("Equipment");
                                }
                            }
                            if (Data.delay_Summand == false){
                                Instantiate.Physics_Enable = false;
                                Instantiate.Position = Object_Summand_Position;
                                Get_Node.AddChild(Instantiate);
                                Instantiate.Physics_Enable = true;
                            }
                            else
                            {
                                Instantiate.Physics_Enable = false;
                                Get_Node.AddChild(Instantiate);
                                await Task.Delay((int)(1000 * Data.delay_SummandTime / 2));
                                Instantiate.Position = Object_Summand_Position;
                                Instantiate.practical_Position = Instantiate.GlobalPosition;
                                Instantiate.Physics_Enable = true;
                            }
                            Data.Present_Object.Add(Instantiate);
                            Data.Current_Summand_Number += 1;
                        }
                        else
                        {
                            await Task.Delay(300);
                        }
                    }
                }
            }
            Data.Summand_ing = false;
        }
    }
    public async static void set_Timer()
    {
        while(Data.Await_Next_Wave_Timer > 0 && Data.Summand_ing == false && Data.Current_WaveTag == WaveTag.Normal && Data.Is_Final_Wave == false)
        {
            await Task.Delay(200);
            Data.Await_Next_Wave_Timer -= 0.2f * (float)Engine.TimeScale;
        }
    }
    /// <summary>
    /// 条件判断生成
    /// </summary>
    public static void Cheak_Summand()
    {
        if (Data.Present_Object.Count <= 0 || Data.Await_Next_Wave_Timer <= 0){
            if (Data.Current_Summand_Value <= Data.Max_Summand_Value - 1 && Data.Summand_ing == false){
                Data.Get_Wave(Data.Current_Summand_Value);
                Summand_Object(Data.Max_Summand_Number);
                Data.Current_Summand_Number = 0;
                Data.Current_Summand_Value += 1;
            }
        }
    }
    /// <summary>
    /// 用于循环逻辑
    /// </summary>
    public static void While_s()
    {
        if (While_Ing == false){return;}
        Cheak_Summand();
        if (Cheak_Tag())
        {
            GD.Print("你过关");
            While_Ing = false;
        }
    }
    /// <summary>
    /// 标签条件判断
    /// </summary>
    public static bool Cheak_Tag()
    {
        if (Data.Current_WaveTag == WaveTag.Normal)
        {
            if (Data.Is_Final_Wave == true && Data.Summand_ing == false)
            {
                if (Data.Present_Object.Count <= 0)
                {
                    return true;
                }
            }
        }else if(Data.Current_WaveTag == WaveTag.Boss)
        {
            if (Data.Allow_victory == true)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 开始生成
    /// </summary>
    public static async void Start_Summand()
    {
        While_Ing = true;
        while (whileMode == WhileMode._While && While_Ing == true){
            While_s();
            await Task.Delay(20);
        }
    }
}