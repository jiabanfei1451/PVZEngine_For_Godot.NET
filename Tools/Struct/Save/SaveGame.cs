using System;
using System.IO;
using System.Text.Json;
using Godot;
using PVZEngine.Data;
using PVZEngine.Static;
namespace PVZEngine.Save;
[GlobalClass]
public partial class SaveGame : Resource
{
    [Export] public SaveMonster MonsterData {get;set;} = new();
    [Export] public SaveLawnData lawnData {get;set;} = new();
    public Summand_Data SummandData {get;set;} = new();
    public void SaveLevel()
    {
        lawnData.Save();
        MonsterData.Save();
        Summand.Data.Save_Random();
    }
    public void LoadLevel()
    {
        lawnData.Load();
        MonsterData.Load();
        Summand.Data.Load_Random();
        Summand.Data.Stop_Summand = false;
    }
    public void Save(String path = "S.res")
    {
        String p = "user://" + $"Saves/{PlayerData.Savedata.Name}/LevelSave/{Level_Script.Level_Object.Chapter_Name}{Level_Script.Data.Level_Name}{Level_Script.Data.LevelID}/" + path;
        path = ProjectSettings.GlobalizePath(p);
        String p2 = "user://" + $"Saves/{PlayerData.Savedata.Name}/LevelSave/{Level_Script.Level_Object.Chapter_Name}{Level_Script.Data.Level_Name}{Level_Script.Data.LevelID}/";
        String path2 = ProjectSettings.GlobalizePath(p2);
        FileSystem.CreateDir(path2);
        try{
            String SaveSummandDataPath = p2+"SummandData.json";
            SummandData = Summand.Data;
            String JsonText = JsonSerializer.Serialize(SummandData);
            var FileS = FileSystem.CreateFile(SaveSummandDataPath);
            String JS = FileSystem.Handle_JsonText(JsonText);
            FileSystem.WriteFileandoff(FileS,JS);
        }
        catch(Exception EX)
        {
            DEBUG.Info.Print(EX);
        }
        finally
        {
            
        }
        DEBUG.Info.Print(path);
        var e = ResourceSaver.Save(this,path);
    }
    public static SaveGame Load(String path = "S.res")
    {
        String p = "user://" +  $"Saves/{PlayerData.Savedata.Name}/LevelSave/{Level_Script.Level_Object.Chapter_Name}{Level_Script.Data.Level_Name}{Level_Script.Data.LevelID}/" + path;
        path = ProjectSettings.GlobalizePath(p);
        String p2 = "user://" + $"Saves/{PlayerData.Savedata.Name}/LevelSave/{Level_Script.Level_Object.Chapter_Name}{Level_Script.Data.Level_Name}{Level_Script.Data.LevelID}/";
        Summand_Data data;
        try{
            var jsonText = FileSystem.Get_FileText(p2);
            data = JsonSerializer.Deserialize<Summand_Data>(jsonText);
            data.Stop_Summand = true;
        }catch(Exception EX)
        {
            DEBUG.Info.PrintErr(EX);
        }
        finally
        {
            
        }
        DEBUG.Info.Print(path);
        return ResourceLoader.Load<SaveGame>(path);
    }
}
