using Godot;
using PVZEngine.Object;
using PVZEngine.Enum;
using PVZEngine.Object.Module;
namespace PVZEngine.Static;
/// <summary>
/// 关卡数据存储
/// </summary>
static class Level_Script : System.Object
{
	/// <summary>
	/// 关卡物体
	/// </summary>
	public static Level_Master_Script Level_Object = null;
	/// <summary>
	/// 数据
	/// </summary>
	public static PVZEngine.Data.LevelData Data = null;
	/// <summary>
	/// 选择对象
	/// </summary>
	public static LevelObject Selected_Object = null;
	/// <summary>
	/// 草坪
	/// </summary>
	public static Godot.ColorRect Lawn;
	public enum Calculation_Type
	{
		add = 0,
		Remove = 1,
		Selection = 2,
	}
	/// <summary>
	/// 获取草坪
	/// </summary>
	/// <param name="Index"></param>
	/// <returns></returns>
	public static Lawn Get_Lawn(Vector2I Index)
	{
		int MaxY = Level_Object.Lawn_Data.Count;
		int MaxX = Level_Object.Lawn_Data[0].Count;
		if (Index.X >= 0 && Index.Y >= 0 && Index.X < MaxX && Index.Y < MaxY)
		{
			return Level_Object.Lawn_Data[Index.Y][Index.X];
		}
		return null;
	}
	/// <summary>
	/// 设置使用
	/// </summary>
	/// <param name="prop"></param>
	public static void Set_Use_porp(Prop prop)
	{
		if (Data.Use_Prop != prop)
		{
			Data.Use_Prop = prop;
		}
		else
		{
			Data.Use_Prop = Prop.Not;
		}
	}
	/// <summary>
	/// 初始化
	/// </summary>
	public static void initialize()
	{
		if (Data is null)
		{
			Data = new();
		}
		Data.Equipment_Capable = 50;
		Data.Star_Fragment = 0;
	}
}