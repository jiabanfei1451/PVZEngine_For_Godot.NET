using PVZEngine.Data;
using Godot;
using PVZEngine.Object.Module;
namespace PVZEngine.Static;

public partial class Tools : Node
{
    public static Node2D Summand_Texture(GlobalData Data)
    {
        //初始化材质
		Node2D texture = Data.Scene.InstantiateOrNull<Node2D>();
		if (texture is LevelObject)
		{
			((LevelObject)texture).Enable = false;
			((LevelObject)texture).Enable_Health = false;
			((LevelObject)texture).Physics_Enable = false;
		}
        return texture;
    }
}