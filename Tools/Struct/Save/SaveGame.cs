using System;
using Godot;
using PVZEngine.Static;
namespace PVZEngine.Save;
[GlobalClass]
public partial class SaveGame : Resource
{
    [Export] public SaveLawnData lawnData {get;set;} = new();
    public void SaveLevel()
    {
        lawnData.SaveLawn();
    }
    public void LoadLevel()
    {
        lawnData.LoadLawn();
    }
    public void Save(String path = "C:/S.res")
    {
        ResourceSaver.Save(this,path);
    }
    public static SaveGame Load(String path = "C:/S.res")
    {
        return ResourceLoader.Load<SaveGame>(path);
    }
}