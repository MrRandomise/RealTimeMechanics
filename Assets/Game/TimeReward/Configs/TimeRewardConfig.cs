using Game.Reward;
using UnityEngine;

namespace Game.TimeReward.Configs
{
    [CreateAssetMenu(menuName = "Lesson/Realtime/MoneyRewardConfig", fileName = "TimeRewardConfig", order = 0)]
    public class TimeRewardConfig : ScriptableObject
    {
        public float Duration = 5f;
        public PlayerReward[] Rewards;
    }
}