using UnityEngine;

public struct AddExperienceSignal
{
    public int Amount { get; }
    // Optional Note for debugging
    public string Message { get; }

    public AddExperienceSignal(int amount, string message = null)
    {
        Amount = amount;
        Message = message;
    }
}
