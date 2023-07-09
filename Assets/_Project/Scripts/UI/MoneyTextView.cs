using Subway.Infrastructure;
using Subway.Logic.Data;
using System;
using TMPro;
using UnityEngine;

namespace Subway.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyTextView : CounterTextView
    {
        private void Start() => Initialize();

        public override void Save()
        {

            Game.Instance.Data.Money = Convert.ToInt32(Value);

            Debug.Log("MONEY SAVED IN HIS METHOD: " + Game.Instance.Data.Money + " and its value: " + Value);
            Debug.Log("And its value to int " + Convert.ToInt32(Value));
        }

        public override void Load()
        {
            Debug.Log("Money loaded in text: " + Game.Instance.Data.Money);
            UpdateText(Game.Instance.Data.Money.ToString());
        }
    }
}
