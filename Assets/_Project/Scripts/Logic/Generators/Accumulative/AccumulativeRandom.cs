using Subway.Logic.Generators.Accumulative.Iterators;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Logic.Generators.Accumulative
{
    /*
     *      отдельный клас: аккмулятивный рандом
            от него ответвление:
            пресет кривой сложности через скриптб обджект.
п           ресет может быть базовым классом с методом Load
     */


    /// <summary>
    ///     Положительное накопительное число.
    ///     Имеет выбор инструмента изменения числа (AccumulativeIterator)
    ///     
    /// </summary>
    ///     Может иметь место рассуждение о Interface Segregation правиле (разбиение на Normalized и классический)
    ///     Но я не вижу смысла использовать ненормализованный класс как обычный счетчик
    public class AccumulativeValue
    {
        private float _accumulatedValue;
        private float _accumulatedValueMax;

        private readonly AccumulativeIterator _accumulativeIterator;

        public float Value => _accumulatedValue;
        public float Normalized => _accumulatedValue / _accumulatedValueMax;

        public AccumulativeValue(AccumulativeIterator accumulativeIterator)
        {
            _accumulatedValue = 0f;
            _accumulatedValueMax = 1f;
            _accumulativeIterator = accumulativeIterator;
        }

        public AccumulativeValue(float initialValue, float maxValue, AccumulativeIterator accumulativeIterator)
        {
            _accumulatedValue = initialValue;
            _accumulatedValueMax = maxValue;
            _accumulativeIterator = accumulativeIterator;
        }

        public float Accumulate()
        {
            _accumulatedValue = 
                Mathf.Clamp(_accumulativeIterator.Next(Normalized), 
                0f, 
                _accumulatedValueMax) * _accumulatedValueMax;

            return _accumulatedValue;
        }

        public void Reset()
        {
            _accumulatedValue = 0f;
            _accumulativeIterator.Reset();
        }
    }
}
