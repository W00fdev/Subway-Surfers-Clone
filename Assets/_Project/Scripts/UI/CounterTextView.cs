using Subway.Infrastructure;
using Subway.Infrastructure.Serivces.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Subway.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CounterTextView : MonoBehaviour, ISavedLoadLogic
    {
        [SerializeField] protected string Prefix;

        protected TextMeshProUGUI Text;
        protected string Value;

        public void Initialize()
        {
            Text = GetComponent<TextMeshProUGUI>();
            // UpdateText("0");
        }

        public void UpdateText(string value)
        {
            Text.text = Prefix + value;
            Value = value;
        }

        public virtual void Save() {}

        public virtual void Load() {}
    }
}
