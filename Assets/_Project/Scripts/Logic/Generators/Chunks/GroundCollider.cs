using Subway.Logic.Data;
using Subway.Logic.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Logic.Chunks
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundCollider : MonoBehaviour
    {
        [SerializeField] private int _groundsCount;

        [SerializeField] private BoxCollider _collider;

        private void Awake()
        {
            //Initialize();
        }

        public void Initialize()
        {
            _collider = GetComponent<BoxCollider>();
            UpdateColliderSize();
        }

        private void OnValidate()
        {
            if (_collider != null)
                UpdateColliderSize();
        }

        private void UpdateColliderSize()
        {
            var colliderSize = Constants.GroundColliderSize;
            colliderSize.z *= _groundsCount;

            _collider.center = colliderSize.Multiply(Constants.GroundColliderOffsetPerSize);
            _collider.size = colliderSize;
        }

    }
}
