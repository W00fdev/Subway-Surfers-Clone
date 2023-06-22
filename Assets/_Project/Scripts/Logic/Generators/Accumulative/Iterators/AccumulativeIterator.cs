using System;
using UnityEngine;
using UnityEngine.Events;

namespace Subway.Logic.Generators.Accumulative.Iterators
{
    public abstract class AccumulativeIterator : ScriptableObject
    {
        [Header("Название итератора")]
        public string Name;
        public float Step;

        protected float Value;

        public UnityEvent SuccessEvent;

        public abstract float Next(float accumulativeValue);
        public abstract void Reset();
    }
}