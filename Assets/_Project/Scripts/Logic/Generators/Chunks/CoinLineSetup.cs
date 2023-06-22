using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

namespace Subway.Logic.Chunks
{
    public class CoinLineSetup : MonoBehaviour
    {
        [SerializeField] private GameObject _moneyPrefab;
        [SerializeField] private int _moneyCount;

        [SerializeField] private Vector3 _moneyOffset;

        private readonly List<GameObject> _spawnedCoins = new List<GameObject>();

        private void Start()
        {
            
        }
        
        public void Initialize(int count)
        {
            _moneyCount = count;
            InstantiateMoney();
        }

        private void OnValidate()
        {
            if (_moneyPrefab == null)
                return;

            Clear();
            InstantiateMoney();
        }

        private void InstantiateMoney()
        {
            var spawnPosition = transform.position;

            for (int i = 0; i < _moneyCount; i++) 
            {
                GameObject money = Instantiate(_moneyPrefab, spawnPosition, Quaternion.identity);
                spawnPosition += _moneyOffset;

                _spawnedCoins.Add(money);
            }
        }

        private void Clear()
        {
            foreach (var money in _spawnedCoins)
                Destroy(money); 
            
            _spawnedCoins.Clear();
        }
    }
}
