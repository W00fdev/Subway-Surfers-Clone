using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Subway
{
    [RequireComponent(typeof(Collider))]
    public class TriggerEventAdapter : MonoBehaviour
    {
        [HideInInspector] 
        public UnityEvent OnTriggerEnterEvent = new UnityEvent();

        private Collider _collider;

        private void Awake() => _collider = GetComponent<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke();
            OnTriggerEnterEvent.RemoveAllListeners();

            _collider.enabled = false;
            //Destroy(gameObject);
        }
    }
}
