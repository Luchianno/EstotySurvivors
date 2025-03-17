using UnityEngine;


public abstract class ScriptableWithDescription : ScriptableObject
{
    public string InternalName;

    public string DisplayName;
    [TextArea]
    public string Description;
}
