using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] AppSettings appSettings;

    public override void InstallBindings()
    {
        var settings = Instantiate(appSettings);
        // settings.LoadFromPreferences();

        Application.targetFrameRate = settings.PreferredFrameRate;

        // app settings
        Container.Bind<AppSettings>().FromInstance(settings).AsSingle();
    }
}