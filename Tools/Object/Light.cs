using Godot;
using PVZEngine.Static;
using System;

namespace PVZEngine.Effect;
public partial class Light : Sprite2D
{
	[Export] public Vector2 LightScale = new(1,1);
	[Export] public Color LightColor = new(1,1,1,1);
	Level_Master_Script level = null;
	[Export] public Node2D Parent_Object = null;
	[Export] public Godot.Vector2 ThisOffset = new();
	[Export] public PVZEngine.Enum.PositionType PositionType = Enum.PositionType.localPosition;
	public void Parent_Object_ExtiTree()
	{
		QueueFree();
	}
	public override void _Ready() {
		base._Ready();
		level = NodeData.Get_Node<Level_Master_Script>("Level");
		if (GetNode("..").Name == "LightColor"){
			
		}
		else if(GetNode("..").Name == "LightMask")
		{
			CanvasItemMaterial itemMaterial = new();
			itemMaterial.BlendMode = CanvasItemMaterial.BlendModeEnum.Sub;
			Material = itemMaterial;
			PackedScene scene = ResourceScene.LoadScene("uid://bqkm0xbe6lus1");
			Light light = scene.Instantiate<Light>();
			light.LightScale = LightScale;
			light.LightColor = LightColor;
			light.Parent_Object = this;
			NodeData.Get_Node<Node2D>("LightColor").AddChild(light);
		}
		if (!(Parent_Object is null))
		{
			Parent_Object.TreeExited += Parent_Object_ExtiTree;
		}
	}
    public override void _Process(double delta)
	{
		if (level == null)
		{
			level = NodeData.Get_Node<Level_Master_Script>("Level");
		}
		Scale = LightScale;
		var Get = GetNodeOrNull("..");
		if (!(Get is null)){
			switch (Get.Name)
			{
				case "LightColor":
					Modulate = new(LightColor.R,LightColor.G,LightColor.B,LightColor.A * level.LightColor_Intensity);
				break;
				case "LightMask":
					Modulate = new(0,0,0,LightColor.A * level.Light_Intensity);
				break;
			}	
		}
		if (!(Parent_Object is null))
		{
			if (PositionType == Enum.PositionType.localPosition){
				Position = Parent_Object.Position + ThisOffset;
			}
			else
			{
				GlobalPosition = Parent_Object.GlobalPosition + ThisOffset;
			}
		}
	}
}
