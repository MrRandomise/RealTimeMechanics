using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Resource
{
    [AddComponentMenu("Gameplay/Player Resource Storage")]
    public abstract class ResourceStorage
    {
        public event Action<int> OnResourceChanged;
        public event Action<int> OnResourceEarned;
        public event Action<int> OnResourceSpent;
        
        public int Resource
        {
            get { return this.resource; }
        }

        [ReadOnly]
        [ShowInInspector]
        private int resource;

        [Title("Methods")]
        [Button]
        [GUIColor(0, 1, 0)]
        public void EarnResource(int amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                throw new Exception($"Can not earn negative resource {amount}");
            }

            var previousValue = this.resource;
            var newValue = previousValue + amount;

            this.resource = newValue;
            this.OnResourceChanged?.Invoke(newValue);
            this.OnResourceEarned?.Invoke(amount);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        public void SpendResource(int amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                throw new Exception($"Can not spend negative resource {amount}");
            }

            var previousValue = this.resource;
            var newValue = previousValue - amount;
            if (newValue < 0)
            {
                throw new Exception(
                    $"Negative resource after spend. Resource in bank: {previousValue}, spend amount {amount} ");
            }

            this.resource = newValue;
            this.OnResourceChanged?.Invoke(newValue);
            this.OnResourceSpent?.Invoke(amount);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        public void SetupMoney(int resource)
        {
            this.resource = resource;
            this.OnResourceChanged?.Invoke(resource);
        }

        public bool CanSpendMoney(int resource)
        {
            return this.resource >= resource;
        }
    }
}