using PVZEngine.Object.Module;
using System;
using PVZEngine.Enum;
namespace PVZEngine.Data;

public class LevelData()
{
    /// <summary>
	/// 关卡ID
	/// </summary>
	public int LevelID {get;set;} = 0;
	/// <summary>
	/// 关卡名称
	/// </summary>
	public String Level_Name {get;set;} = "第一天";
	public bool Card_Drag {get;set;} = false;
	/// <summary>
	/// 器械能
	/// </summary>
	public short Equipment_Capable {get;set;} = 200;
	/// <summary>
	/// 星之碎片数量
	/// </summary>
	public byte Star_Fragment {get;set;} = 0;
	/// <summary>
	/// 音高
	/// </summary>
	public byte audio_Scale {get;set;} = 0;
	/// <summary>
	/// 当前使用道具
	/// </summary>
	public Prop Use_Prop {get;set;} = Prop.Not;
}