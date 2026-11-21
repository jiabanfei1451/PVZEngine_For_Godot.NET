using System.Collections.Generic;
using Godot;

namespace PVZEngine.Data;
[GlobalClass]
public partial class SummandPositionData : Resource
{
    [Export] public Godot.Vector2 SummandPosition {get;set;} = new(720,0);
    [Export] public Godot.Vector2I SummandScoop {get;set;} = new(0,4);
    [Export] public PVZEngine.Enum.ObjectType Summand_ObjectType {get;set;} = Enum.ObjectType.Null;
}