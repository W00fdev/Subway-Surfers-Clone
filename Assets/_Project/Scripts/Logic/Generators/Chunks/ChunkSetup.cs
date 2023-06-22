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

        private Dictionary<ChunkPart, float> _percentageData;


        // Временное решение до внедрения архитектуры
        private void Start()
        {

        }

        // Инициализирует части уровня по составленным процентам
        public void Initialize(/*Dictionary<ChunkPart, float> percentageData*/)
        {
            foreach (var part in _levelParts)
                part.ChooseVariant(0.5f);

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
