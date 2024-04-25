using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.View.UI.View
{
    public sealed class ResourceView : MonoBehaviour
    {
        [PropertyOrder(-10)]
        [ReadOnly]
        [ShowInInspector]
        public int Amount { get; private set; }

        [Space]
        [SerializeField]
        private TMP_Text amountText;

        [SerializeField]
        private RectTransform iconTransform;

        public void Setup(int amount)
        {
            this.Amount = amount;
            this.UpdateAmountText();
        }

        public void Increment(int range)
        {
            var newAmount = this.Amount + range;

            this.Amount = newAmount;
            this.UpdateAmountText();
            this.AnimateIncome();
        }

        public void Decrement(int range)
        {
            var newAmount = this.Amount - range;
            this.Amount = newAmount;
            this.UpdateAmountText();
        }

        private void UpdateAmountText()
        {
            var amount = Math.Max(this.Amount, 0);
            this.amountText.text = amount.ToString();
        }

        public Vector3 GetIconPosition()
        {
            return this.iconTransform.TransformPoint(this.iconTransform.rect.center);
        }

        private void AnimateIncome()
        {
            var rootTransform = this.iconTransform;
            DOTween.Sequence()
                .Append(rootTransform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
                .Append(rootTransform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f));
        }
    }
}