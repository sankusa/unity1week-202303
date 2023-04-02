using UnityEngine;
using Zenject;
using Sankusa.unity1week202303.Domain;
using Sankusa.unity1week202303.Presentation;

public class InGameSceneInstaller : MonoInstaller
{
    [SerializeField] private InGameCamera inGameCamera;
    [SerializeField] private CommandMaster commandMaster;
    [SerializeField] private BattlePerformer battlePerformer;
    [SerializeField] private FinishPanel finishPanel;
    public override void InstallBindings()
    {
        // Domain
        Container
            .BindInterfacesAndSelfTo<GameTimer>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<Faith>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<FinishFlag>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<BattleManager>()
            .AsSingle()
            .NonLazy();

        // Presentation
        Container
            .BindInterfacesAndSelfTo<CommandMaster>()
            .FromInstance(commandMaster)
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<HumanManager>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<CommandInvoker>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<InGameCamera>()
            .FromInstance(inGameCamera)
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<BattlePerformer>()
            .FromInstance(battlePerformer)
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<FinishPanel>()
            .FromInstance(finishPanel)
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<InGameLoop>()
            .AsSingle()
            .NonLazy();
    }
}