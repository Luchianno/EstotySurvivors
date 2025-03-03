using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public bool ForceOnScreenJoystick = false;

    [SerializeField] GameObject enemyPrefab;

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

        Container.Bind<EnemySpawningArea>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<EnemySpawner>().FromComponentInHierarchy(true).AsSingle();

        // UI 
        // enemy damage popup pool
        Container.BindFactory<string, Color, Vector3, TextPopup, TextPopup.Factory>()
            .FromMonoPoolableMemoryPool(x =>
                x.WithInitialSize(10)
                .FromComponentInNewPrefab(textPopupPrefab)
                .UnderTransformGroup("EnemyDamagePopups")
            );
    }
}