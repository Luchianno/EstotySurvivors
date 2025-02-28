using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public bool ForceOnScreenJoystick = false;

    public override void InstallBindings()
    {
        if(Application.isEditor && !ForceOnScreenJoystick)
        {
            Container.BindInterfacesAndSelfTo<PlayerInputKeyboard>().AsSingle();
        }
        else
        {
            Container.BindInterfacesAndSelfTo<PlayerInputJoystick>().AsSingle();
        }

        
    }
}