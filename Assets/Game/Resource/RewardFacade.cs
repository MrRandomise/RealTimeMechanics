using System;
using Game.Reward;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Resource
{
    public class RewardFacade
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly DiamondStorage _diamondStorage;
        private readonly WoodStorage _woodStorage;
        private readonly StoneStorage _stoneStorage;
        private readonly IronStorage _ironStorage;
        private readonly CardStorage _cardStorage;

        public RewardFacade(DiContainer diContainer)
        {
            _moneyStorage = diContainer.Resolve<MoneyStorage>();
            _diamondStorage = diContainer.Resolve<DiamondStorage>();
            _woodStorage = diContainer.Resolve<WoodStorage>();
            _stoneStorage = diContainer.Resolve<StoneStorage>();
            _ironStorage = diContainer.Resolve<IronStorage>();
            _cardStorage = diContainer.Resolve<CardStorage>();
            diContainer.Inject(this);
        }

        public void RewardPlayer(PlayerReward[] playerRewards)
        {
            var reward = playerRewards[Random.Range(0, playerRewards.Length)];
            ResourceStorage storage = reward.RewardType switch
            {
                RewardType.Money => _moneyStorage,
                RewardType.Diamond => _diamondStorage,
                RewardType.Wood => _woodStorage,
                RewardType.Stone => _stoneStorage,
                RewardType.Iron => _ironStorage,
                RewardType.Card => _cardStorage,
                _ => throw new ArgumentOutOfRangeException()
            };

            storage?.EarnResource(reward.RewardAmount);
        }
    }
}