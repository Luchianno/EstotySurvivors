using SimpleInputNamespace;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] bool ForceOnScreenJoystick = false;

    [SerializeField] string joystickParent = "HUD";

    [SerializeField] Joystick joystick;

    public override void InstallBindings()
    {
        if (Application.isEditor && !ForceOnScreenJoystick)
        {
            Container.BindInterfacesAndSelfTo<PlayerInputKeyboard>().AsSingle();
        }
        else
        {
            Container.BindInterfacesAndSelfTo<PlayerInputJoystick>().AsSingle();
            Container.Bind<Joystick>().FromComponentInNewPrefab(joystick)
                .UnderTransformGroup(joystickParent)
                .AsSingle()
                .NonLazy();

                
        }
    }
}