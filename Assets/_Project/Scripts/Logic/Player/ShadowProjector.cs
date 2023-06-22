using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Logic.Player
{
    /// <summary>
    /// Скрипт на персонаже, использует его позицию, перемещает Shadow
    /// Должен быть настроена маска слоя и расстояние.
    /// </summary>
    public class ShadowProjector : MonoBehaviour
    {
        public Transform Shadow;
        public LayerMask LayerForShadow;
        public float RayDistance;

        // максимум 1 объект: пол
        private readonly RaycastHit[] _hits = new RaycastHit[1];

        // В будущем даже стейт не нужен для завершения 
        private void Update()
        {
            Ray ray = new Ray(transform.position, -transform.up);
            if (Physics.RaycastNonAlloc(ray, _hits, RayDistance, LayerForShadow.value) > 0)
            {
                Shadow.position = new Vector3
                    (transform.position.x, _hits[0].point.y, transform.position.z);
            }
        }
    }
}
