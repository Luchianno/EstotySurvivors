using UnityEngine;
using Zenject;

public class ExperienceManager : MonoBehaviour
{
    [Inject] SignalBus signalBus;

    public int Experience { get; protected set; }

    public void Reset()
    {
        Experience = 0;
    }

    public void AddExperience(int amount)
    {
        Experience += amount;
    }

}
