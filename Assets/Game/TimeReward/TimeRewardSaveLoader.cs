using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Zenject;

namespace Game.TimeReward
{
    public class TimeRewardSaveLoader
    {
        private HashSet<IRealtimeTimer> _realtimeTimers = new HashSet<IRealtimeTimer>();

        public void OnLoadGame(IRealtimeTimer[] timers)
        {
            _realtimeTimers = new HashSet<IRealtimeTimer>(timers);

            foreach (var realtimeTimer in _realtimeTimers)
            {
                Load(realtimeTimer);
            }
        }
        
        public void RegisterTimer(IRealtimeTimer realtimeTimer)
        {
            _realtimeTimers.Add(realtimeTimer);
            realtimeTimer.TimerStarted += OnTimerStarted;
        }

        public void UnregisterTimer(IRealtimeTimer realtimeTimer)
        {
            _realtimeTimers.Remove(realtimeTimer);
            realtimeTimer.TimerStarted -= OnTimerStarted;
        }
        

        private void OnTimerStarted(string id)
        {
            var now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(id, now);
            
            Debug.Log("Save timer");
        }

        private void Load(IRealtimeTimer timer)
        {
            var offlineTime = CalculateOfflineTime(timer.SaveKey);
            timer.SynchronizeTime(offlineTime);
            
            Debug.Log($"PAUSE SECONDS: {offlineTime}");
        }

        private float CalculateOfflineTime(string saveKey)
        {
            var savedTime = PlayerPrefs.GetString(saveKey);
            DateTime time = DateTime.Parse(savedTime, CultureInfo.InvariantCulture);

            var now = DateTime.Now;
            TimeSpan timeSpan = now - time;
            return (float)timeSpan.TotalSeconds;
        }
    }
}