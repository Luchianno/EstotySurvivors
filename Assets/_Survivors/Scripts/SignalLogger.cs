using System;
using UnityEngine;
using Zenject;

// listens to all signals and logs them to the console
public class SignalLogger : MonoBehaviour
{
    [Inject] SignalBus signalBus;

    void OnEnable()
    {
        // props
        signalBus.Subscribe<PropPickedSignal>(OnPropPickedSignal);

        // game events
        signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChangedSignal);
        signalBus.Subscribe<PlayerExperienceGainedSignal>(OnPlayerExperienceGainedSignal);

    }

    void OnPropPickedSignal(PropPickedSignal signal)
    {
        Debug.Log($"Prop picked: {signal.Prop.DisplayName} with Value: {signal.Prop.Value}");
    }

    void OnGameStateChangedSignal(GameStateChangedSignal signal)
    {
        Debug.Log($"Game state changed: {signal.NewState}");
    }

    void OnPlayerExperienceGainedSignal(PlayerExperienceGainedSignal signal)
    {
        Debug.Log($"Player experience gained: {signal.Amount} level progress: {signal.Progress}");
    }
}
