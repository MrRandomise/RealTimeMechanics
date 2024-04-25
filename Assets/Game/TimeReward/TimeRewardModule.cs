using System;
using System.Collections.Generic;
using Game.Resource;
using Game.TimeReward.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.TimeReward
{
    public class TimeRewardModule : MonoBehaviour
    {
        [SerializeField] private TimeRewardConfig _timeRewardConfig;
        
        [ShowInInspector]
        public readonly TimeReward _timeReward = new TimeReward();
        
        [Inject]
        public void Ctor(RewardFacade rewardFacade, TimeRewardSaveLoader timeRewardSaveLoader)
        {
            _timeReward.Construct(_timeRewardConfig, rewardFacade);
            timeRewardSaveLoader.RegisterTimer(_timeReward);
        }

        public void Start()
        {
            _timeReward.StartGame();
        }

        
        public void ReceiveReward()
        {
            _timeReward.ReceiveReward();
        }

        public IEnumerable<TimeReward> GetElements()
        {
            yield return _timeReward;
        }

        public IEnumerable<object> GetServices()
        {
            yield return _timeReward;
        }
    }
}