using Game.Resource;
using Game.View.UI.Presenter;
using Game.View.UI.View;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ResourcesInstaller : MonoBehaviour
    {
        [SerializeField] private ResourceView moneyView;
        [SerializeField] private ResourceView diamondView;
        [Space]
        [SerializeField] private ResourceView woodView;
        [SerializeField] private ResourceView stoneView;
        [SerializeField] private ResourceView ironView;
        [Space]
        [SerializeField] private ResourceView cardView;

        [Inject]
        public void Ctor(DiContainer diContainer)
        {
            var moneyListener = new ResourceListener(diContainer.Resolve<MoneyStorage>(), moneyView);
            var diamondListener = new ResourceListener(diContainer.Resolve<DiamondStorage>(), diamondView);
            var woodListener = new ResourceListener(diContainer.Resolve<WoodStorage>(), woodView);
            var stoneListener = new ResourceListener(diContainer.Resolve<StoneStorage>(), stoneView);
            var ironListener = new ResourceListener(diContainer.Resolve<IronStorage>(), ironView);
            var cardListener = new ResourceListener(diContainer.Resolve<CardStorage>(), cardView);
        }
    }
}