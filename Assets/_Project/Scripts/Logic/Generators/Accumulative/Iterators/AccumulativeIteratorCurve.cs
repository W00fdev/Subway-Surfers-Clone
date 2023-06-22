using UnityEngine;

namespace Subway.Logic.Generators.Accumulative.Iterators
{
    [CreateAssetMenu(fileName = "AccumulativeIteratorCurve", menuName = "Data/Iterators/AccumulativeIteratorCurve", order = 1)]
    public class AccumulativeIteratorCurve : AccumulativeIterator
    {
        public AnimationCurve AnimationCurve;

        [Header("Масштаб графика: ")]
        public float CurveScaleFactor = 1f;

        // Success Event выполняется по достижени конца
        public override float Next(float accumulativeValueNormalized)
        {
            Value = Mathf.Clamp(
                Value + Step,
                0f, 
                CurveScaleFactor);

            if (Value == CurveScaleFactor)
            {
                // Заканчиваем накопительный эффект когда прошли по всей кривой.
                SuccessEvent?.Invoke();
                return AnimationCurve.Evaluate(CurveScaleFactor);
            }

            return AnimationCurve.Evaluate(Value);
        }

        public override void Reset()
        {
            Value = 0f;
        }
    }
}