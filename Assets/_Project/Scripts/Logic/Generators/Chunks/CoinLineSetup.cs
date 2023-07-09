using Subway.Infrastructure;
using Subway.Logic.Data;
using Subway.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Subway.Logic.Chunks
{
    public class CoinLineSetup : MonoBehaviour
    {
        [SerializeField] private GameObject _moneyPrefab;
        [SerializeField] private int _moneyCount;

        [SerializeField] private Vector3 _moneyOffset;

        private MoneyTextView _moneyTextView;
        private readonly List<GameObject> _spawnedCoins = new List<GameObject>();

        private void Start()
        {
            
        }
        
        public void Initialize(MoneyTextView moneyTextView/*int count*/)
        {
            _moneyTextView = moneyTextView;
            InstantiateMoney();
        }

        private void InstantiateMoney()
        {
            var spawnPosition = transform.position;

            for (int i = 0; i < _moneyCount; i++)
            {
                GameObject money = Instantiate(_moneyPrefab, spawnPosition, Quaternion.identity);
                spawnPosition += _moneyOffset;

                InitializeMoney(money);

                _spawnedCoins.Add(money);
            }
        }

        private void InitializeMoney(GameObject money)
        {
            Destroyable moneyDestroyable = money.GetComponent<Destroyable>();
            TriggerEventAdapter moneyTriggerAdapter = money.GetComponent<TriggerEventAdapter>();

            //UnityAction addMoney = AddMoney;
            moneyTriggerAdapter.OnTriggerEnterEvent.AddListener(() =>
            {
                Game.Instance.Data.Money += 1;
                _moneyTextView.UpdateText(Game.Instance.Data.Money.ToString());
                moneyDestroyable.DestroyImmediately();
            });
        }


        private void Clear()
        {
            foreach (var money in _spawnedCoins)
                Destroy(money); 
            
            _spawnedCoins.Clear();
        }

    }
}
