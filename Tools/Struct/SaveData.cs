using System;
using Godot;
namespace PVZEngine.Data;
public struct SaveData()
{
    public Godot.Collections.Dictionary<String,Variant> data {get;set;}
}