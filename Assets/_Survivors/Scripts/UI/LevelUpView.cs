using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class LevelUpView : UIView
{
    [SerializeField] float stayAfterOpeningChest = 3f;
    [SerializeField] List<UpgradeElement> upgradeElements;
    [SerializeField] AudioClip selectedSound;

    [Inject] SignalBus signalBus;

    protected override void Awake()
    {
        base.Awake();

        signalBus.Subscribe<PlayerLevelUpSignal>(OnPlayerLevelUp);

        foreach (var element in upgradeElements)
        {
            element.OnClickEvent.AddListener(OnUpgradeElementClicked);
        }
    }

    private void OnPlayerLevelUp(PlayerLevelUpSignal signal)
    {
        if (signal.Upgrades.Count == 0)
        {
            Debug.LogError("No upgrades available for level up.");
            return;
        }

        SetUpgrades(signal.Upgrades);
    }

    void OnUpgradeElementClicked(UpgradeData data)
    {
        ClickedRoutine().Forget();

        async UniTaskVoid ClickedRoutine()
        {
            // disable all elements first
            panel.interactable = false;

            signalBus.Fire(new PlaySfxSignal(selectedSound));

            // Wait for the animation to finish before hiding the view
            await UniTask.WaitForSeconds(stayAfterOpeningChest);

            signalBus.Fire(new UpgradeSelectedSignal(data));
            Hide();
        }
    }

    void SetUpgrades(List<UpgradeData> upgrades)
    {
        if (upgrades.Count > upgradeElements.Count)
        {
            Debug.LogError("Not enough upgrade elements to display all upgrades.");
            return;
        }

        // disable all elements first
        foreach (var element in upgradeElements)
        {
            element.gameObject.SetActive(false);
        }

        // enable and set the upgrades
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgradeElements[i].gameObject.SetActive(true);
            upgradeElements[i].Set(upgrades[i]);
        }
    }


}
