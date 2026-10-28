using Godot;

namespace PVZEngine.Static.Default;
/// <summary>
/// 适用于LevelObject的检测预设
/// </summary>
public static class LevelCheckdefault
{
    /// <summary>
    /// 预设排除
    /// </summary>
    /// <returns></returns>
    public static Godot.Collections.Array<StringName> defaultExclude_Group()
    {
        return new(){"Projectile","Area","Flight"};
    }
    /// <summary>
    /// 器械检测预设
    /// </summary>
    /// <returns></returns>
    public static Godot.Collections.Array<StringName> defaultDetection_Group_Equipment()
    {
        return new(){"Monster"};
    }
    /// <summary>
    /// 怪物检测预设
    /// </summary>
    /// <returns></returns>
    public static Godot.Collections.Array<StringName> defaultDetection_Group_Monster()
    {
        return new(){"Equipment"};
    }
    /// <summary>
    /// 默认检测预设
    /// </summary>
    /// <returns></returns>
    public static Godot.Collections.Array<StringName> defaultDetection_Group()
    {
        return new(){"Equipment","Monster"};
    }
}