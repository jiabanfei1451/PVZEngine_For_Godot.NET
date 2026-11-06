using System;
using PVZEngine.UI;
using PVZEngine.Enum;
using Godot;
using PVZEngine.Static;
namespace PVZEngine.Data;
/// <summary>
/// 卡槽数据
/// </summary>
public partial class Card_Data : Node
{
	[Signal] public delegate void Selected_ChangeEventHandler(Card card);


	[Export] public Godot.Collections.Array<Godot.Collections.Array> Data = new Godot.Collections.Array<Godot.Collections.Array>()
	{
	// 源器械读取0
	new Godot.Collections.Array(){},
	// 消耗1
	new Godot.Collections.Array(){},
	// 冷却2
	new Godot.Collections.Array(){},
	// 第一次冷却减免3
	new Godot.Collections.Array(){},
	// 展示坐标偏移4
	new Godot.Collections.Array(){},
	// 缩放5
	new Godot.Collections.Array(){},
	// 光标坐标偏移6
	new Godot.Collections.Array(){},
	// 地图坐标偏移7
	new Godot.Collections.Array(){},
	// 地图大小8
	new Godot.Collections.Array(){},
	// 物体类型9
	new Godot.Collections.Array(){},
	// 移动端材质大小10
	new(){},
	// 移动端材质偏移11
	new(){},
	// 是否为事件卡12
	new(){},
	// 事件标签13
	new(){},
	};
	/// <summary>
	/// 已选择卡槽的剩余冷却
	/// </summary>
	[Export] public Godot.Collections.Array<float> Selected_CD = new Godot.Collections.Array<float>()
	{};
	/// <summary>
	/// 选卡物体原数据
	/// </summary>
	[Export] public Godot.Collections.Array Selected = [new Node(),new Node(),new Node(),new Node(),new Node(),new Node(),new Node(),new Node(),new Node(),new Node()];
	/// <summary>
	/// 选择卡的原数据
	/// </summary>
	[Export] public Card Selected_raw_Object = null;
	/// <summary>
	/// 当前卡槽的原数据
	/// </summary>
	Card Current_Selected_raw_Object = null;
	/// <summary>
	/// 已拥有的器械
	/// </summary>
	[Export] public Godot.Collections.Array<int> Obtained_Data = new Godot.Collections.Array<int>(){0,1,2,3,4,5};
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		if (Current_Selected_raw_Object != Selected_raw_Object)
		{
			Current_Selected_raw_Object = Selected_raw_Object;
			EmitSignal("Selected_Change",Current_Selected_raw_Object);
		}
    }

	public override void _Ready() {
		base._Ready();
		Initialization();
		#region 增加器械数据
		GlobalData data = new()
		{
			Scene = ResourceScene.LoadScene("res://Object/Equipment/Transmitter.tscn"),
			Sonsume = 100,
			CD = 7.5f,
			First_Time_RemoveCD = 3,
			Scale = new(2,2),
			Offset = new(64,87)
		};
		Add_Data(data);
		data = new()
		{
			Scene = ResourceScene.LoadScene("res://Object/Equipment/Furnace.tscn"),
			Sonsume = 50,
			CD = 7.5f,
			First_Time_RemoveCD = 7.5f,
			Scale = new(2,2),
			Offset = new(72,87)
		};
		Add_Data(data);
		data = new()
		{
			Scene = ResourceScene.LoadScene("res://Object/Monster/Zombies.tscn"),
			Sonsume = -50,
			CD = 0f,
			First_Time_RemoveCD = 0f,
			Scale = new(2,2),
			Offset = new(80,186),
			Map_Offset = new(40,70),
			Object_Type = ObjectType.Monster
		};
		Add_Data(data);
		data = new()
		{
			Scene = ResourceScene.LoadScene("res://Object/Equipment/Obsidian.tscn"),
			Sonsume = 50,
			CD = 30f,
			First_Time_RemoveCD = 15f,
			Scale = new(2,2),
			Offset = new(73,94),
		};
		Add_Data(data);
		data = new()
		{
			Scene = ResourceScene.LoadScene("uid://lyb0noko5sk1"),
			Sonsume = 0,
			CD = 0,
			First_Time_RemoveCD = 0,
			Scale = new(2,2),
			Offset = new(65,87),
			Object_Type = ObjectType.Bearing_unit
		};
		Add_Data(data);
		data = new()
		{
			Scene = ResourceScene.LoadScene("res://Object/Equipment/好友/HuanChong152.tscn"),
			Sonsume = 0,
			CD = 0,
			First_Time_RemoveCD = 0,
			Scale = new(2,2),
			Offset = new(64,87),
		};
		Add_Data(data);
		data = new()
		{
			Scene = ResourceScene.LoadScene("uid://bokxlltcu2pxm"),
			Sonsume = 0,
			CD = 0,
			First_Time_RemoveCD = 0,
			Scale = new(2,2),
			Offset = new(64,87),
		};
		Add_Data(data);
		data = new()
		{
			Scene = ResourceScene.LoadScene("uid://bcfm88f3tbgt8"),
			Sonsume = 0,
			CD = 0,
			First_Time_RemoveCD = 0,
			Scale = new(2,2),
			Offset = new(64,87),
		};
		Add_Data(data);
		#endregion
	}
	public void CD_Initialization()
	{
		Selected_CD.Resize(Data[0].Count);
		for (int i = 0 ; i < Selected_CD.Count; i++)
		{
			Selected_CD[i] = 0;
		}
	}
	/// <summary>
	/// 初始化
	/// </summary>
	public void Initialization()
	{
		Selected_raw_Object = null;
		Selected.Clear();
		for (int Count = 0;Count < PlayerData.Savedata.Card_Quantity; Count++)
		{
			Control New_Null = new Control();
			New_Null.Name = "Null";
			New_Null.QueueFree();
			Selected.Add(New_Null);
		}
	}
	/// <summary>
	/// 获取卡槽索引
	/// </summary>
	/// <param name="Node"></param>
	/// <returns></returns>
	public int Get_Card_Index(Control @Node = null)
	{
		int Index = -1;
		Index = Selected.IndexOf(@Node);
		return Index;
	}
	/// <summary>
	/// 移除卡槽
	/// </summary>
	/// <param name="node"></param>
	public void Remove_Card_Index(Control @node = null)
	{
		if (PVZEngine.Static.Cheak.Cheak_Null<Control>(@node)){return;}
		Selected.Remove(@node);
		arrange_Card();
	}
	/// <summary>
	/// 添加卡槽
	/// </summary>
	/// <param name="node"></param>
	public void Add_Card_Index(Control @node = null)
	{
		if (PVZEngine.Static.Cheak.is_null<Control>(@node)){return;}
		Selected.Add(@node);
		arrange_Card();
	}
	/// <summary>
	/// 排列
	/// </summary>
	public int Get_Selected_Card_Len()
	{
		int dex = -1;
		foreach(Control control in Selected)
		{
			if(!Cheak.is_null<Control>(control))
			{
				dex += 1;
			}
		}
		return dex;
	}
	/// <summary>
	/// 排列卡槽
	/// </summary>
	public void arrange_Card()
	{
		Godot.Collections.Array variants = [];
		foreach (Control node in Selected)
		{
			if (!Cheak.is_null<Control>(node))
			{
				variants.Add(node);
			}
		}
		for (int Length = variants.Count;Length < PlayerData.Savedata.Card_Quantity; Length++)
		{
			Control New_Null = new Control();
			New_Null.QueueFree();
			variants.Add(New_Null);
		}
		Selected = variants;
	}
	#region 数据管理
	/// <summary>
	/// 获取卡槽数据
	/// </summary>
	/// <param name="Index"></param>
	/// <returns></returns>
		/// <summary>
	/// 添加数据
	/// </summary>
	/// <param name="Scene">物体</param>
	/// <param name="sonsume">消耗</param>
	/// <param name="CD">冷却</param>
	/// <param name="RemoveCD">游戏开始时减少冷却</param>
	/// <param name="Scale">大小</param>
	/// <param name="Offset">材质偏移</param>
	/// <param name="Map_Scale">地图大小</param>
	/// <param name="Map_Offset">地图偏移</param>
	/// <param name="Mouse_Offset">鼠标偏移</param>
	/// <param name="Object_Type">物体类型</param>
	/// <param name="Reliant_UUID">依赖ID</param>
	/// <param name="Reliant_Tag">依赖标签</param>
	public void Add_Data(GlobalData data){
		Data[0].Add(data.Scene);
		Data[1].Add(data.Sonsume);
		Data[2].Add(data.CD);
		Data[3].Add(data.First_Time_RemoveCD);
		Data[4].Add(data.Offset);
		Data[5].Add(data.Scale);
		Data[6].Add(data.Mouse_Offset);
		Data[7].Add(data.Map_Offset);
		Data[8].Add(data.Map_Scale);
		Data[9].Add((int)data.Object_Type);
		Data[10].Add(data.Pe_Scale);
		Data[11].Add(data.Pe_Offset);
		Data[12].Add(data.is_EventCard);
		Data[13].Add(data.EventTag);
	}
	#endregion
	/// <summary>
	/// 获取卡槽数据
	/// </summary>
	/// <param name="tree"></param>
	/// <returns></returns>
	public static Card_Data Get_Card_Data(SceneTree tree)
	{
		Card_Data Node = tree.Root.GetNode<Card_Data>("CardData");
		return Node;
	}
	/// <summary>
	/// 获取索引数据
	/// </summary>
	/// <param name="Index"></param>
	/// <returns></returns>
	public static GlobalData Get_CardData(int Index)
	{
		Godot.Collections.Array<Godot.Collections.Array> Data = Get_GlobalNode.Get_Card_Data().Data;
		if (Index > -1)
		{
			PackedScene scene = (PackedScene)Data[0][Index];
			short sonsume = (short)Data[1][Index];
			float cd = (float)Data[2][Index];
			float removeCD = (float)Data[3][Index];
			Godot.Vector2 offset = (Godot.Vector2)Data[4][Index];
			Godot.Vector2 scale = (Godot.Vector2)Data[5][Index]; 
			Godot.Vector2 mouse_Offset = (Godot.Vector2)Data[6][Index];
			Godot.Vector2 map_Offset = (Godot.Vector2)Data[7][Index];
			Godot.Vector2 map_Scale = (Godot.Vector2)Data[8][Index];
			ObjectType object_Type = GlobalData.GetObjectType((int)Data[9][Index]);
			Godot.Vector2 pe_Offset = (Godot.Vector2)Data[11][Index];
			Godot.Vector2 pe_Scale = (Godot.Vector2)Data[10][Index];
			bool is_EventCard = (bool)Data[12][Index];
			String EventTag = (String)Data[13][Index];
			GlobalData Back = new GlobalData()
			{
				Scene = scene,
				CD = cd,
				Sonsume = sonsume,
				First_Time_RemoveCD = removeCD,
				Offset = offset,
				Scale = scale,
				Mouse_Offset = mouse_Offset,
				Map_Offset = map_Offset,
				Map_Scale = map_Scale,
				Object_Type = object_Type,
				Pe_Scale = pe_Scale,
				Pe_Offset = pe_Offset,
				is_EventCard = is_EventCard,
				EventTag = EventTag
			};
			return Back;
		}
		else{return GlobalData.Zero();}
	}
	/// <summary>
	/// 场景搜索方法
	/// </summary>
	/// <param name="Index"></param>
	/// <returns></returns>
	public static GlobalData Get_CardData(PackedScene Index)
	{
		Godot.Collections.Array<Godot.Collections.Array> Data = Get_GlobalNode.Get_Card_Data().Data;
		return Get_CardData(Data[0].IndexOf(Index));
	}
	#region Class
	/// <summary>
	/// 卡槽贴图实例
	/// </summary>
	/// <param name="Texture_PC">电脑端卡槽背景贴图</param>
	/// <param name="Texture_Border_PC_Focus">电脑端卡槽选中边框贴图</param>
	/// <param name="Texture_Border_PC">电脑端卡槽边框贴图</param>
	/// <param name="Texture_PE">手机端卡槽背景贴图</param>
	/// <param name="Texture_Border_PE_Focus">手机端卡槽选中边框贴图</param>
	/// <param name="Texture_Border_PE">手机端卡槽边框贴图</param>
	public class Card_Texture(Image @Texture_PC,Image @Texture_Border_PC,Image @Texture_Border_PC_Focus,Image @Texture_PE,Image @Texture_Border_PE,Image @Texture_Border_PE_Focus)
	{
		/// <summary>
		/// 电脑端卡槽背景贴图
		/// </summary>
		Image Texture_PC = @Texture_PC;
		/// <summary>
		/// 电脑端卡槽选中边框贴图
		/// </summary>
		Image Texture_Border_PC_Focus = @Texture_Border_PC_Focus;
		/// <summary>
		/// 电脑端卡槽边框贴图
		/// </summary>
		Image Texture_Border_PC = @Texture_Border_PC;
		/// <summary>
		/// 手机端卡槽背景贴图
		/// </summary>
		Image Texture_PE = @Texture_PE;
		/// <summary>
		/// 手机端卡槽选中边框贴图
		/// </summary>
		Image Texture_Border_PE_Focus = @Texture_Border_PE_Focus;
		/// <summary>
		/// 手机端卡槽边框贴图
		/// </summary>
		Image Texture_Border_PE = @Texture_Border_PE;
	}

	#endregion
}
