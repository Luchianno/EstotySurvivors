using UnityEngine;

public struct UIViewSignal
{
    public UISignalType SignalType { get; }
    public string ViewName { get; }

    public UIViewSignal(UISignalType signalType, string viewName)
    {
        ViewName = viewName;
        SignalType = signalType;
    }
   
}
