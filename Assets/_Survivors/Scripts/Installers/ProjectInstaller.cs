using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] AppSettings appSettings;

    public override void InstallBindings()
    {
        
        // app settings
        Container.Bind<AppSettings>().FromInstance(Instantiate(appSettings)).AsSingle();
    }
}