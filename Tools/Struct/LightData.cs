using Godot;
namespace PVZEngine.Data;
[GlobalClass]
/// <summary>
/// 光源数据
/// </summary>
public partial class LightData : Resource
{
    [Export] public Color LightColor = new(1,1,1,1);
    [Export] public Vector2 Offset = new();
    [Export] public Vector2 Scale = new(1,1);
    /// <summary>
    /// 物体路径
    /// </summary>
    [Export] public NodePath Parene_Object = ".";
    [Export] public PVZEngine.Enum.PositionType positionType = Enum.PositionType.localPosition;
}