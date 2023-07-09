using System;
using System.Collections.Generic;
using UnityEngine;
using Subway.Infrastructure.Serivces.SaveLoad;
using Subway.UI;

namespace Subway.Infrastructure.Serivces
{
    public class GameFactory
    {
        private readonly GameObject _playerPrefab;
        private readonly GameObject _chunkGeneratorPrefab;
        private readonly GameObject _hudPrefab;

        public readonly List<ISavedLogic> SavedObjects;

        public GameFactory(GameObject playerPrefab, GameObject chunkGeneratorPrefab, GameObject hudPrefab)
        {
            _playerPrefab = playerPrefab;
            _chunkGeneratorPrefab = chunkGeneratorPrefab;
            _hudPrefab = hudPrefab;

            SavedObjects = new List<ISavedLogic>();
        }

        public GameObject CreateChunkGenerator() => GameObject.Instantiate(_chunkGeneratorPrefab);

        public GameObject CreateHero(Vector3 at, Transform parent = null) 
            => GameObject.Instantiate(_playerPrefab, at, Quaternion.identity, parent);

        public GameObject CreateHud(Transform parent = null)
        {
            var hud = GameObject.Instantiate(_hudPrefab, Vector3.zero, Quaternion.identity, parent);
            var collectionSaved = hud.GetComponentsInChildren<ISavedLogic>();

            foreach (var saved in collectionSaved)
                (saved as CounterTextView).Initialize();

            SavedObjects.AddRange(collectionSaved);

            return hud;
        }
    }
}
