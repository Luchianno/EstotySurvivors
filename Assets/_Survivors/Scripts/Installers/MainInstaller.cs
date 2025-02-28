using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public bool ForceOnScreenJoystick = false;

    [SerializeField] GameObject fastEnemyPrefab;
    [SerializeField] GameObject slowEnemyPrefab;

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

        // enemy pool
        Container.BindMemoryPool<EnemyUnit, EnemyUnit.Pool>()
            .WithInitialSize(20)
            .FromComponentInNewPrefab(fastEnemyPrefab)
            .UnderTransformGroup("Enemies");

        Container.Bind<EnemySpawningArea>().FromComponentInHierarchy(true).AsSingle();
    }
}