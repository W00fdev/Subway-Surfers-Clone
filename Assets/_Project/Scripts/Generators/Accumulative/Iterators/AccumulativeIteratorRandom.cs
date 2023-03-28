using UnityEngine;

namespace Subway.Generators.Accumulative.Iterators

{
    [CreateAssetMenu(fileName = "AccumulativeIteratorRandom", menuName = "Data/Iterators/AccumulativeIteratorRandom", order = 1)]
    public class AccumulativeIteratorRandom : AccumulativeIterator
    {
        public override float Next(float accumulativeValueNormalized)
        {
            if (Random.value <= accumulativeValueNormalized)
            {
                // Обнуляем накопительный эффект когда выбили шанс.
                SuccessEvent?.Invoke();
                return 0f;
            }

            return accumulativeValueNormalized + Step;
        }

        public override void Reset()
        {
        }
    }
}