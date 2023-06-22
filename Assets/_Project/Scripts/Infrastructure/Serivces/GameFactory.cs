using System;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Infrastructure.Serivces
{
    public class GameFactory
    {
        private readonly GameObject _playerPrefab;
        private readonly GameObject _chunkGeneratorPrefab;

        public GameFactory(GameObject playerPrefab, GameObject chunkGeneratorPrefab)
        {
            _playerPrefab = playerPrefab;
            _chunkGeneratorPrefab = chunkGeneratorPrefab;
        }

        public GameObject CreateChunkGenerator()
            => GameObject.Instantiate(_chunkGeneratorPrefab);

        public GameObject CreateHero(Vector3 at, Transform parent = null)
            => GameObject.Instantiate(_playerPrefab, at, Quaternion.identity, parent);
    }
}
