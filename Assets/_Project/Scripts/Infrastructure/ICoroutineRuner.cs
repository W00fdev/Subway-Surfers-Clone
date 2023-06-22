using System.Collections;
using UnityEngine;

namespace Subway.Infrastructure
{
    public interface ICoroutineRuner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}