using UnityEngine;

public struct PropPickedSignal 
{
    public PropData Prop { get; }

    public PropPickedSignal(PropData prop)
    {
        Prop = prop;
    }

    public override string ToString()
    {
        return $"PropPicked: {Prop.InternalName}";
    }

}
