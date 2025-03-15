using UnityEngine;

public struct TauntSignal 
{
    public string Taunt{ get; }
    public Vector3 Position { get; }

    public TauntSignal(string taunt, Vector3 position)
    {
        Taunt = taunt;
        Position = position;
    }

}