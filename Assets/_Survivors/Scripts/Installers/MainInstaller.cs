using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public bool ForceOnScreenJoystick = false;

    [Space]
    [SerializeField] LevelProgression levelProgression;

    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject propItemPrefab;
    [SerializeField] GameObject textPopupPrefab;

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

        // find player game object and bind it
        var player = GameObject.FindGameObjectWithTag("Player");
        Container.Bind<Transform>().WithId("Player").FromInstance(player.transform).AsSingle();

        Container.Bind<EnemySpawningArea>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<EnemySpawner>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<PropSpawner>().FromComponentInHierarchy(true).AsSingle();

        Container.Bind<EnemyMovementSystem>().FromComponentInHierarchy(true).AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerStatsManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<ExperienceManager>().AsSingle();

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
            .FromMonoPoolableMemoryPool(x =>
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

        // UI 
        // text popup pool
        Container.BindFactory<string, Color, Vector3, TextPopup, TextPopup.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(textPopupPrefab)
                .UnderTransformGroup("TextPopups")
            );

        #endregion


        #region UI screens

        Container.Bind<LandingScreen>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<DeathScreen>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<WinScreen>().FromComponentInHierarchy(true).AsSingle();

        #endregion


        #region Signals

        // player signals
        Container.DeclareSignal<PlayerDeathSignal>();
        Container.DeclareSignal<PlayerDamageSignal>();
        Container.DeclareSignal<PlayerHealSignal>();
        Container.DeclareSignal<PlayerExperienceGainedSignal>();
        Container.DeclareSignal<PlayerLevelUpSignal>();
        Container.DeclareSignal<PlaySfxSignal>();

        // enemy signals
        Container.DeclareSignal<EnemySpawnedSignal>().OptionalSubscriber();
        Container.DeclareSignal<EnemyDamageSignal>().OptionalSubscriber();
        Container.DeclareSignal<EnemyDeathSignal>().OptionalSubscriber();

        // prop signals
        Container.DeclareSignal<PropPickedSignal>();

        // general signals
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<AddExperienceSignal>(); 



        #endregion

    }
}