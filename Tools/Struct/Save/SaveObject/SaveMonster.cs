using System;
using Godot;
using PVZEngine.Static;

namespace PVZEngine.Save;
[GlobalClass]
public partial class SaveMonster : SaveResource
{
    [Export] public Godot.Collections.Array<PackedScene> Scenes {get;set;} = new();

    [Export] public String NodePath = "Monster";
    public override void Save()
    {
        base.Save();
        var get = NodeData.Get_Node<Node2D>(NodePath);
        if (get is null){return;}
        var GetAllMonster = get.GetTree().GetNodesInGroup("Monster");
        foreach(var Node in GetAllMonster)
        {
            if (Node.GetNode("..") is Node2D){
                GD.Print(Node.GetPath(),"Save GlobalPosition:",((Node2D)Node).GlobalPosition,"Position:",((Node2D)Node).Position);
                PackedScene Scene = new();
                Scene.Pack(Node);
                Scenes.Add(Scene);
            }
        }
    }
    public override void Load()
    {
        base.Load();
        Node2D GetMonsterNode = NodeData.Get_Node<Node2D>(NodePath);
        if (GetMonsterNode is null){return;}
        if (Summand.Data is null){return;}
        var ClearNode = GetMonsterNode.GetChildren();
        foreach(var Clear in ClearNode)
        {
            Clear.QueueFree();
        }
        Summand.Data.Present_Object.Clear();
        foreach(PackedScene scene in Scenes)
        {
            var Instantiate = scene.Instantiate<PVZEngine.Object.Module.LevelObject>();
            Summand.Data.Present_Object.Add(Instantiate);
            Instantiate.Physics_Enable = false;
            GetMonsterNode.AddChild(Instantiate);
            Instantiate.Reset_Position();
            Instantiate.Physics_Enable = true;
        }
    }
}