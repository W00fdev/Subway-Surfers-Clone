using Subway.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Logic.Chunks
{
    public class ChunkSetup : MonoBehaviour
    {
        public Transform EndPivot;

        [SerializeField] private List<AlternativeParts> _levelParts;
        [SerializeField] private List<CoinLineSetup> _coins;

        [SerializeField] private TriggerEventAdapter _endOfTheChunk;

        private Destroyable _destroyable;

        private Dictionary<ChunkPart, float> _percentageData;


        // Временное решение до внедрения архитектуры
        private void Awake()
        {
            _destroyable = GetComponent<Destroyable>();

            
        }

        // Инициализирует части уровня по составленным процентам
        public void Initialize(Action reachedEndAction, MoneyTextView moneyTextView)
        {
            _endOfTheChunk.OnTriggerEnterEvent.AddListener(() => reachedEndAction?.Invoke());
            _endOfTheChunk.OnTriggerEnterEvent.AddListener(() => _destroyable.DestroyIn(seconds: 3f));

            foreach (var part in _levelParts)
                part.ChooseVariant(0.5f);

            foreach (var coin in _coins)
                coin.Initialize(moneyTextView);

            /*            _percentageData = percentageData;
                        foreach (var part in _percentageData)
                            _levelParts.Find((a) => a.ChunkPart == part.Key).ChooseVariant(part.Value);*/
        }
    }

    // Улучшить через секвенцию взятия: блокеры, паверапы, потом поезда, которые их содержат.
    // Либо вложить блокеры внутрь вариантов
    // Но мы просто заполним в нужной последовательности в инспекторе
    // Даже скорее всего, это самостоятельные компоненты, которые при инициализации вырубаются саим
    public enum ChunkPart { Trains = 0, PowerUps, Blockers}

    [Serializable]
    public class AlternativeParts
    {
        public ChunkPart ChunkPart;

        public GameObject Variant1;
        public GameObject Variant2;

        public void ChooseVariant(float percentage)
        {
            bool activeVariant = (UnityEngine.Random.value <= percentage);
            
            Variant1.SetActive(activeVariant);
            Variant2.SetActive(!activeVariant);
        }
    }
}
