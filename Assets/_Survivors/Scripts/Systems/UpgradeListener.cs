using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeListener : MonoBehaviour
{
    [field: SerializeField]
    public List<UpgradeData> SelectedUpgrades { get; protected set; } = new List<UpgradeData>();

    [Inject] SignalBus signalBus;
    [Inject(Id = "Player")] Transform player;


    PlayerWeapon playerWeapon;

    void Start()
    {
        signalBus.Subscribe<UpgradeSelectedSignal>(OnUpgradeSelected);

        playerWeapon = player.GetComponent<PlayerWeapon>();
    }

    void OnUpgradeSelected(UpgradeSelectedSignal signal)
    {
        switch (signal.UpgradeData.Type)
        {
            case UpgradeType.PiercingBullets:
                playerWeapon.WeaponData.BulletData.IsPiercing = true;
                break;
            case UpgradeType.DoubleBullets:
                playerWeapon.WeaponData.BulletsPerShot = 2;
                playerWeapon.WeaponData.SpreadDegrees = 30;
                break;
            case UpgradeType.TripleBullets:
                playerWeapon.WeaponData.BulletsPerShot = 3;
                playerWeapon.WeaponData.SpreadDegrees = 45;
                break;
            case UpgradeType.MovementSpeed:
                player.GetComponent<PlayerMovementController>().MovementSpeed += 2;
                break;
            case UpgradeType.ExplodingBullets:
                playerWeapon.WeaponData.BulletData.IsExplosive = true;
                break;
            case UpgradeType.BulletDamage:
                playerWeapon.WeaponData.BulletData.DamageMin += 1;
                playerWeapon.WeaponData.BulletData.DamageMax += 1;
                break;
            case UpgradeType.GemMagnet:
                Debug.Log("GemMagnet not implemented");
                break;

            default:
                Debug.LogError("UpgradeType not Implemented: " + signal.UpgradeData.Type);
                break;
        }

        SelectedUpgrades.Add(signal.UpgradeData);
    }
}
