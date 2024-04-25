using System;
using System.Collections;
using System.Threading;
using Sirenix.OdinInspector;
using UnityEngine;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;

namespace Elementary
{
    [Serializable]
    public sealed class Countdown : ICountdown, ISerializationCallbackReceiver
    {
        public event Action OnStarted;
        public event Action OnTimeChanged;
        public event Action OnStopped;
        public event Action OnEnded;
        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-9)]
        [ProgressBar(0, 1)]
        public float Progress
        {
            get { return 1 - this._remainingTime / this.duration; }
            set { this.SetProgress(value); }
        }

        public float Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float RemainingTime
        {
            get { return this._remainingTime; }
            set { this._remainingTime = Mathf.Clamp(value, 0, this.duration); }
        }
        
        [Space]
        [SerializeField]
        private float duration;

        private float _remainingTime;
        private bool _isForceStopped;
        
        public Countdown()
        {
        }

        public Countdown(float duration)
        {
            this.duration = duration;
            this._remainingTime = duration;
        }
        
        [Button]
        public void Play()
        {
            if (this.IsPlaying)
            {
                return;
            }

            this.IsPlaying = true;
            this.OnStarted?.Invoke();
            TimerRoutine().Forget();
        }

        [Button]
        public void Stop()
        {
            if (!this.IsPlaying) return;

            _isForceStopped = true;
            this.IsPlaying = false;
            this.OnStopped?.Invoke();
        }
        
        [Button]
        public void ResetTime()
        {
            this._remainingTime = this.duration;
            this.OnReset?.Invoke();
        }

        private async UniTask TimerRoutine()
        {
            _isForceStopped = false;
            
            while (this._remainingTime > 0 && !_isForceStopped)
            {
                await UniTask.Yield();
                this._remainingTime -= Time.deltaTime;
                this.OnTimeChanged?.Invoke();
            }

            if (_isForceStopped)
            {
                _isForceStopped = false;
                return;
            }

            this.IsPlaying = false;
            this.OnEnded?.Invoke();
        }

        private void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this._remainingTime = this.duration * (1 - progress);
            this.OnTimeChanged?.Invoke();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this._remainingTime = this.duration;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}