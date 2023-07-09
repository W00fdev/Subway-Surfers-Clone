using System;
using System.Collections;
using UnityEngine;

namespace Subway.Logic
{
    public class Destroyable : MonoBehaviour
    {
        public void DestroyImmediately() => Destroy(gameObject);
        public void DestroyIn(float seconds) => StartCoroutine(DestroyRoutine(seconds));

        IEnumerator DestroyRoutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

    }
}
