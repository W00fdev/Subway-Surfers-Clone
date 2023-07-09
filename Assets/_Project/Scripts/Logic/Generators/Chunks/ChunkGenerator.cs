using Subway.Logic.Chunks;
using Subway.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Subway.Logic.Generators.Chunks
{
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField] private List<ChunkSetup> _chunksPrefabs;
        [SerializeField] private ChunkSetup _firstChunk;

        [SerializeField] private int _firstChunkSpawned;

        private MoneyTextView _moneyTextView;
        private ChunkSetup _currentChunk;

        private void Start()
        {
            //Initialize();
        }

        public void Initialize(MoneyTextView moneyTextView)
        {
            _moneyTextView = moneyTextView;

            _currentChunk = Instantiate(_firstChunk, Vector3.zero, Quaternion.identity);
            _currentChunk.Initialize(SpawnChunk, _moneyTextView);

            for (int i = 0; i < _firstChunkSpawned; i++) 
                SpawnChunk();
        }

        private void SpawnChunk()
        {
            var chunkIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0, _chunksPrefabs.Count - 0.6f));
            var spawnedChunk = Instantiate(_chunksPrefabs[chunkIndex], _currentChunk.EndPivot.position, Quaternion.identity);
            spawnedChunk.Initialize(SpawnChunk, _moneyTextView);

            _currentChunk = spawnedChunk;
        }
    }
}
