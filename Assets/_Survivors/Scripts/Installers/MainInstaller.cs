using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public bool ForceOnScreenJoystick = false;

    public override void InstallBindings()
    {
        if(Application.isEditor && !ForceOnScreenJoystick)
        {
            Container.Bind<IPlayerInput>().To<PlayerInputKeyboard>().AsSingle();
        }
        else
        {
            Container.Bind<IPlayerInput>().To<PlayerInputJoystick>().AsSingle();
        }

    }
}