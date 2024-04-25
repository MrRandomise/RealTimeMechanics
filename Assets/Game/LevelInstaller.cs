using Game.Resource;
using Game.TimeReward;
using UnityEngine;
using Zenject;

namespace Game
{
    public class LevelInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<TimeRewardSaveLoader>().AsSingle().NonLazy();

            Container.Bind<MoneyStorage>().AsSingle().NonLazy();
            Container.Bind<DiamondStorage>().AsSingle().NonLazy();
            Container.Bind<WoodStorage>().AsSingle().NonLazy();
            Container.Bind<StoneStorage>().AsSingle().NonLazy();
            Container.Bind<IronStorage>().AsSingle().NonLazy();
            Container.Bind<CardStorage>().AsSingle().NonLazy();
            Container.Bind<RewardFacade>().AsSingle().NonLazy();
        }
    }
}