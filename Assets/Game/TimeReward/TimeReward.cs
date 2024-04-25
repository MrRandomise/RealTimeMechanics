using System;
using Elementary;
using Game.Resource;
using Game.TimeReward.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.TimeReward
{
    public class TimeReward : IRealtimeTimer
    {
        public event Action<string> TimerStarted;

        public string SaveKey => nameof(TimeReward);

        private TimeRewardConfig _timeRewardConfig;
        private RewardFacade _rewardFacade;

        [ShowInInspector]
        private Countdown _timer = new Countdown();

        public void Construct(TimeRewardConfig timeRewardConfig, RewardFacade moneyStorage)
        {
            _timeRewardConfig = timeRewardConfig;
            _rewardFacade = moneyStorage;
            _timer.Duration = _timeRewardConfig.Duration;
            _timer.RemainingTime = _timeRewardConfig.Duration;
        }
        
        public void StartGame()
        {
            PlayTimer();
        }
        
        private void PlayTimer()
        {
            if (_timer.Progress == 0f)
            {
                Debug.Log("Timer started");
                TimerStarted?.Invoke(SaveKey);
            }
            
            _timer.Play();
        }
        
        public void SynchronizeTime(float time)
        {
            _timer.RemainingTime -= time;
        }
        
        [Button]
        public void ReceiveReward()
        {
            if (CanReceiveReward())
            {
                Debug.Log("You received reward");
                _rewardFacade.RewardPlayer(_timeRewardConfig.Rewards);
                _timer.ResetTime();
                PlayTimer();
            }
            else
            {
                Debug.Log("You can't receive reward");
            }
        }

        private bool CanReceiveReward()
        {
            return _timer.Progress >= 1f;
        }
    }
}
