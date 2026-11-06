using System;
using System.Collections.Generic;
using Godot;

namespace PVZEngine.AutoLoad;
public partial class TimeScale : Node
{
    public String TimeScaleType = "Normal";
    public System.Collections.Generic.Dictionary<String,Double> ScaleTypes = new(){{"Normal" , 1d},{"Stop" , 0d}};
    public String Temp = "";
    public override void _Ready() {
        base._Ready();
        Game.Get_GlobalNode.TimeScale = this;
    }
    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (Temp != TimeScaleType)
        {
            Temp = TimeScaleType;
            if (ScaleTypes.Keys.Contains(TimeScaleType))
            {
                Engine.TimeScale = ScaleTypes[TimeScaleType];
            }
            else
            {
                ScaleTypes.Add(TimeScaleType,1d);
                if (ScaleTypes.Keys.Contains(TimeScaleType))
                {
                    Engine.TimeScale = ScaleTypes[TimeScaleType];
                }
            }
        }
        else
        {
            if (Engine.TimeScale != ScaleTypes[TimeScaleType])
            {
                if (ScaleTypes.Keys.Contains(TimeScaleType))
                {
                    Engine.TimeScale = ScaleTypes[TimeScaleType];
                }
            }
            return;
        }
    }
}