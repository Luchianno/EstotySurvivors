using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public bool ForceOnScreenJoystick = false;

    [Space]
    [SerializeField] LevelProgression levelProgression;
    [SerializeField] UpgradeProgression upgradeProgression;

    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject propItemPrefab;
    [SerializeField] GameObject bulletExplosionPrefab;
    [Space]
    [SerializeField] GameObject damagePopupPrefab;
    [SerializeField] GameObject tauntPopupPrefab;

    public override void InstallBindings()
    {
        #region Player Input

        if (Application.isEditor && !ForceOnScreenJoystick)
        {
            Container.BindInterfacesAndSelfTo<PlayerInputKeyboard>().AsSingle();
        }
        else
        {
            Container.BindInterfacesAndSelfTo<PlayerInputJoystick>().AsSingle();
        }

        #endregion


        #region Level, Player, Enemies, Props        

        Container.BindInterfacesAndSelfTo<LevelProgression>().FromInstance(Instantiate(levelProgression)).AsSingle();
        Container.BindInterfacesAndSelfTo<UpgradeProgression>().FromInstance(Instantiate(upgradeProgression)).AsSingle();

        // find player game object and bind it
        var player = GameObject.FindGameObjectWithTag("Player");
        Container.Bind<Transform>().WithId("Player").FromInstance(player.transform).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMovementController>().FromComponentOn(player).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerWeapon>().FromComponentOn(player).AsSingle();

        Container.BindInterfacesAndSelfTo<SpawningArea>().FromComponentInHierarchy(true).AsSingle();
        Container.BindInterfacesAndSelfTo<EnemySpawner>().FromComponentInHierarchy(true).AsSingle();
        Container.BindInterfacesAndSelfTo<PropSpawner>().FromComponentInHierarchy(true).AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyMovementSystem>().FromComponentInHierarchy(true).AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerStatsManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<ExperienceManager>().AsSingle();

        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();

        #endregion


        #region UI

        // FYI, UI Screens are injected through "ZenjectBinding" component on their respective GameObjects

        #endregion


        #region Pools

        // enemy pool
        Container.BindFactory<Vector3, EnemyData, EnemyUnit, EnemyUnit.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransformGroup("Pools/Enemies")
            );

        // bullet factory
        Container.BindFactory<Vector2, Vector2, BulletData, SimpleBulletBehaviour, SimpleBulletBehaviour.Factory>()
            .FromPoolableMemoryPool<Vector2, Vector2, BulletData, SimpleBulletBehaviour, BulletPool>(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(bulletPrefab)
                .UnderTransformGroup("Pools/Bullets")
            );

        // prop factory
        Container.BindFactory<Vector2, PropData, PropItem, PropItem.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(5)
                .FromComponentInNewPrefab(propItemPrefab)
                .UnderTransformGroup("Pools/Props")
            );

        Container.BindFactory<Vector2, BulletExplosion, BulletExplosion.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(5)
                .FromComponentInNewPrefab(bulletExplosionPrefab)
                .UnderTransformGroup("Pools/Explosions")
            );

        // UI 
        // damage text popup pool
        Container.BindFactory<string, Color, Vector3, TextPopup, TextPopup.Factory>()
            .WithId("DamagePopup")
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(damagePopupPrefab)
                .UnderTransformGroup("TextPopups")
            );

        // taunt text popup pool
        Container.BindFactory<string, Color, Vector3, TextPopup, TextPopup.Factory>()
            .WithId("TauntPopup")
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(tauntPopupPrefab)
                .UnderTransformGroup("TextPopups")
            );

        #endregion


        #region Signals

        // signal bus
        SignalBusInstaller.Install(Container);

        // player signals
        Container.DeclareSignal<PlayerDeathSignal>();
        Container.DeclareSignal<PlayerDamageSignal>();
        Container.DeclareSignal<PlayerHealSignal>();
        Container.DeclareSignal<PlayerExperienceGainedSignal>();
        Container.DeclareSignal<PlayerLevelUpSignal>();
        Container.DeclareSignal<PlaySfxSignal>();

        Container.DeclareSignal<UpgradeSelectedSignal>();

        // enemy signals
        Container.DeclareSignal<EnemySpawnedSignal>().OptionalSubscriber();
        Container.DeclareSignal<EnemyDamageSignal>().OptionalSubscriber();
        Container.DeclareSignal<EnemyDeathSignal>().OptionalSubscriber();

        // prop signals
        Container.DeclareSignal<PropPickedSignal>();

        // general signals
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<AddExperienceSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<EndGameSignal>();

        // UI signals
        Container.DeclareSignal<UIViewSignal>();



        #endregion

    }
}