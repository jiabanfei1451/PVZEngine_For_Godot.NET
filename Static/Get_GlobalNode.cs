
using Godot;
using System;
using PVZEngine.AutoLoad;
using AutoLoad;
using PVZEngine.Data;
using PVZEngine.Module;
namespace PVZEngine.Static{
/// <summary>
/// 获取全局节点
/// </summary>
static class Get_GlobalNode
{
	public static SceneTree Tree = null;
		/// <summary>
	/// 实体列表
	/// </summary>
	public static Object_List object_List = null;
	/// <summary>
	/// 获取卡槽数据
	/// </summary>
	/// <param name="tree"></param>
	/// <returns></returns>
	public static Card_Data Get_Card_Data(SceneTree tree = null)
	{
		if (PVZEngine.Static.Cheak.is_null<SceneTree>(Tree)){
			Tree = tree;
		}
		Card_Data Node = Tree.Root.GetNode<Card_Data>("CardData");
		return Node;
	}
	/// <summary>
	/// 获取音乐引擎
	/// </summary>
	/// <param name="tree"></param>
	/// <returns></returns>
	public static Muisc_Engine Get_Muisc_Engine(SceneTree tree = null)
	{
		if (PVZEngine.Static.Cheak.is_null<SceneTree>(Tree)){
			Tree = tree;
		}
		Muisc_Engine muisc_Engine = Tree.Root.GetNode<Muisc_Engine>("MuiscEngine");
		return muisc_Engine;
	}
	/// <summary>
	/// 获取音频列表
	/// </summary>
	/// <param name="tree"></param>
	/// <returns></returns>
	public static Audio_List Get_Audio_List(SceneTree tree = null){
		if (PVZEngine.Static.Cheak.is_null<SceneTree>(Tree)){
			Tree = tree;
		}
		Audio_List audio_List = Tree.Root.GetNode<Audio_List>("AudioList");
		return audio_List;	
	}
}
}
