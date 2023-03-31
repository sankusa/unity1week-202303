using UnityEngine;
using Zenject;
using SankusaLib.SceneManagementLib;

namespace Sankusa.unity1week202303.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SceneArgStore>()
                .AsSingle()
                .NonLazy();
        }
    }
}