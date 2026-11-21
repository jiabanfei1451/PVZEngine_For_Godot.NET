using Godot;
namespace PVZEngine.Data;

public partial class LawnSummandData : Resource
{
    /// <summary>
	/// 草坪生成数组
	/// </summary>
	[Export] public Godot.Collections.Array<Godot.Collections.Array<int>> Lawn_Array {get;set;} = new Godot.Collections.Array<Godot.Collections.Array<int>>()
	{
		new Godot.Collections.Array<int>(){0,0,0,0,0,0,0,0,0},
		new Godot.Collections.Array<int>(){0,0,0,0,0,0,0,0,0},
		new Godot.Collections.Array<int>(){0,0,0,0,0,0,0,0,0},
		new Godot.Collections.Array<int>(){0,0,0,0,0,0,0,0,0},
		new Godot.Collections.Array<int>(){0,0,0,0,0,0,0,0,0},
	};
	/// <summary>
	/// 草坪每次生成后的偏移
	/// </summary>
	[Export] public Godot.Vector2 Lawn_Spawn_Offect {get;set;} = new Godot.Vector2(80,80);
	/// <summary>
	/// 草坪开始生成坐标
	/// </summary>
	[Export] public Godot.Vector2 Lawn_Spawn_Position {get;set;} = new Godot.Vector2(-428,-181);
	/// <summary>
	/// 草坪偏移
	/// </summary>
	[Export] public Godot.Collections.Array<Godot.Collections.Array<Godot.Vector2>> Lawn_Offset_Array {get;set;} = new Godot.Collections.Array<Godot.Collections.Array<Godot.Vector2>>([[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[]]);
}