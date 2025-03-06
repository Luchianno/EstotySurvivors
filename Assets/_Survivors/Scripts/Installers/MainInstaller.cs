using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public bool ForceOnScreenJoystick = false;

    [SerializeField] AppSettings appSettings;
    [Space]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject textPopupPrefab;

    public override void InstallBindings()
    {
        if (Application.isEditor && !ForceOnScreenJoystick)
        {
            Container.BindInterfacesAndSelfTo<PlayerInputKeyboard>().AsSingle();
        }
        else
        {
            Container.BindInterfacesAndSelfTo<PlayerInputJoystick>().AsSingle();
        }

        // app settings
        Container.Bind<AppSettings>().FromInstance(Instantiate(appSettings)).AsSingle();

        // * Signals *
        // player signals
        Container.DeclareSignal<PlayerDeathSignal>();
        Container.DeclareSignal<PlayerDamageSignal>();
        Container.DeclareSignal<PlayerLevelUpSignal>();
        Container.DeclareSignal<PlaySfxSignal>();

        // enemy signals
        Container.DeclareSignal<EnemyDamageSignal>().OptionalSubscriber();
        Container.DeclareSignal<EnemyDeathSignal>().OptionalSubscriber();

        // find player game object and bind it
        var player = GameObject.FindGameObjectWithTag("Player");
        Container.Bind<Transform>().WithId("Player").FromInstance(player.transform).AsSingle();

        // enemy pool
        Container.BindFactory<Vector3, EnemyData, EnemyUnit, EnemyUnit.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransformGroup("Enemies")
            );

        // bullet factory
        Container.BindFactory<Vector2, Vector2, BulletData, SimpleBulletBehaviour, SimpleBulletBehaviour.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(bulletPrefab)
                .UnderTransformGroup("Bullets")
            );

        Container.Bind<EnemySpawningArea>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<EnemySpawner>().FromComponentInHierarchy(true).AsSingle();

        // UI 
        // text popup pool
        Container.BindFactory<string, Color, Vector3, TextPopup, TextPopup.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(textPopupPrefab)
                .UnderTransformGroup("TextPopups")
            );
    }
}