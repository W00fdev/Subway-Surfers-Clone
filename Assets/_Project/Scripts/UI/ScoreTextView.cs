using Subway.Infrastructure;
using Subway.Logic.Data;
using System;
using TMPro;
using UnityEngine;

namespace Subway.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreTextView : CounterTextView
    {
        private void Start() => Initialize();

        public override void Save() => Game.Instance.Data.Score = Convert.ToInt32(Value);

        public override void Load() => UpdateText(Game.Instance.Data.Score.ToString());
    }
}
