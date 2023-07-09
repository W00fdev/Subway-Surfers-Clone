using Subway.Logic.Generators.Chunks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Infrastructure
{

    /// <summary>
    /// This script is set in Script Execution Order
    /// </summary>
    public class Bootstrapper : MonoBehaviour, ICoroutineRuner
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _chunkGenerator;
        [SerializeField] private GameObject _hudPrefab;

        private void Awake()
        {
            Game game = new Game(this, _playerPrefab, _chunkGenerator, _hudPrefab);
            DontDestroyOnLoad(gameObject);
        }
    }
}
