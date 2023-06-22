using Subway.Infrastructure.Serivces;
using Subway.Logic.Generators.Chunks;
using UnityEngine;

namespace Subway.Infrastructure
{
    public class Game
    {
        private readonly ICoroutineRuner _coroutineRuner;
        private readonly GameObject _playerPrefab;
        private readonly GameObject _chunkGeneratorPrefab;
        private readonly SceneLoader _sceneLoader;

        private readonly GameFactory _gameFactory;

        public Game(ICoroutineRuner coroutineRuner, GameObject playerPrefab, GameObject chunkGeneratorPrefab)
        {
            _coroutineRuner = coroutineRuner;
            _playerPrefab = playerPrefab;
            _chunkGeneratorPrefab = chunkGeneratorPrefab;

            _sceneLoader = new SceneLoader      (_coroutineRuner);
            _sceneLoader.LoadInitialScene(GameLoop);

            _gameFactory = new GameFactory(_playerPrefab, _chunkGeneratorPrefab);

            // Здесь же выход в меню, загрузка данных и пр может быть
        }

        private void GameLoop()
        {
            // Здесь происходит основное действие игры
            // Initialize Objects

            ChunkGenerator chunkGenerator = _gameFactory.CreateChunkGenerator()
                .GetComponent<ChunkGenerator>();
            _gameFactory.CreateHero(Vector3.zero);

            chunkGenerator.Initialize();


            // Pause if

            // Continue if

            // Exit game

            // Ressurect player

            // Stop player because of death
        }
    }
}
