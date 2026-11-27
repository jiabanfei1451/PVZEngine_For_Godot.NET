using System;
using Godot;
using PVZEngine.Object;
using PVZEngine.Object.Module;
using PVZEngine.Static;

namespace PVZEngine.Save;
[GlobalClass]
public partial class SaveLawnData : SaveResource
{
    [Export] public Godot.Collections.Array<Vector2I> Key {get;set;} = new(){};
    [Export] public Godot.Collections.Array<Godot.Collections.Array<String>> LawnKey {get;set;} = new(){};
    [Export] public Godot.Collections.Array<Godot.Collections.Array<PackedScene>> Scene {get;set;} = new(){};
    [Export] public Godot.Collections.Array<Godot.Collections.Array<String>> LawnKey2 {get;set;} = new(){};
    [Export] public Godot.Collections.Array<Godot.Collections.Array<PackedScene>> Scene2 {get;set;} = new(){};
    public override void Save()
    {
        base.Save();
        var GetLawn = NodeData.Get_Node<Node2D>("Lawn").GetTree().GetNodesInGroup("Lawn");
        int Index = 0;
        foreach(Lawn lawn in GetLawn)
        {

            LawnKey.Add(new());
            Scene.Add(new());
            Key.Add(lawn.LawnIndex);
            foreach(var p in lawn.Current_Object.Objects)
            {
                try{
                    LawnKey[Index].Add(p.Key);
                    PackedScene Sc = new PackedScene();
                    Sc.Pack(p.Value);
                    Scene[Index].Add(Sc);
                }catch(Exception EX)
                {
                    DEBUG.Info.PrintErr(EX);
                }
                finally
                {
                    
                }
            }
            LawnKey2.Add(new());
            Scene2.Add(new());
            foreach(var p in lawn.Current_Object.Misc_Objects)
            {
                try{
                    LawnKey2[Index].Add(p.Key.ToString());
                    PackedScene Sc = new PackedScene();
                    Sc.Pack(p.Value);
                    Scene2[Index].Add(Sc);
                }catch(Exception EX)
                {
                    DEBUG.Info.PrintErr(EX);
                }
                finally
                {
                    
                }
            }
            Index += 1;
        }
    }
    public override void Load()
    {
        base.Load();
        int SceneIndex = 0;
        DEBUG.Info.Print(Key);
        foreach(Vector2I Index in Key)
        {
            DEBUG.Info.Print(Index);
            Lawn GetLawn = Level_Script.Level_Object.Lawn_Data[Index.Y][Index.X];
            try{
                foreach(var p in Level_Script.Level_Object.Lawn_Data[Index.Y][Index.X].Current_Object.Objects.Values)
                {
                    p.QueueFree();
                }
                foreach(var p in Level_Script.Level_Object.Lawn_Data[Index.Y][Index.X].Current_Object.Misc_Objects.Values)
                {
                    p.QueueFree();
                }
            }catch(Exception EX)
            {
                DEBUG.Info.PrintErr(EX);
            }
            finally
            {
                
            }
            GetLawn.Current_Object.Misc_Objects.Clear();
            GetLawn.Current_Object.Objects.Clear();

            for(int I = 0;I < Scene[SceneIndex].Count; I++)
            {
                try{
                    var Instantiate = Scene[SceneIndex][I].Instantiate<Equipment>();
                    NodeData.Get_Node<Node2D>("Equipment").AddChild(Instantiate);
                    GetLawn.Current_Object.Objects.Add(LawnKey[SceneIndex][I],Instantiate);
                    Handle_Object(Instantiate);
                }catch(Exception EX)
                {
                    DEBUG.Info.PrintErr(EX);
                }
                finally
                {
                    
                }
            }
            for(int I = 0;I < Scene2[SceneIndex].Count; I++)
            {
                if (Scene2[SceneIndex].Count == 0)
                {
                    try{
                        var Instantiate = Scene2[SceneIndex][I].InstantiateOrNull<Equipment>();
                        NodeData.Get_Node<Node2D>("Equipment").AddChild(Instantiate);
                        GetLawn.Current_Object.Misc_Objects.Add(LawnKey2[SceneIndex][I],Instantiate);
                        Handle_Object(Instantiate);
                    }catch(Exception EX)
                    {
                        DEBUG.Info.PrintErr(EX);
                    }
                    finally
                    {
                        
                    }
                }
            }
            SceneIndex += 1;
        }
    }
    public void Handle_Object(ObjectPhysics Handle)
    {
        if (Handle is MVZ2.Object.Equipment.Landmine)
        {
            ((MVZ2.Object.Equipment.Landmine)Handle).Timer.Stop(); 
            ((MVZ2.Object.Equipment.Landmine)Handle).Excavated(); 
        }
    }
}