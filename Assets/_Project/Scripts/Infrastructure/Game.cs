using Subway.Infrastructure.Serivces.SaveLoad;
using Subway.Logic.Generators.Chunks;
using Subway.Infrastructure.Serivces;
using Subway.Logic.Camera;
using Subway.Logic.Data;
using UnityEngine;
using System;
using Subway.UI;

namespace Subway.Infrastructure
{
    public class Game
    {
        private readonly ICoroutineRuner _coroutineRuner;
        private readonly GameObject _playerPrefab;
        private readonly GameObject _chunkGeneratorPrefab;
        private readonly GameObject _hudPrefab;
        private readonly SceneLoader _sceneLoader;

        public readonly ISaveLoadProgressService SaveLoadProgressService;
        public readonly PlayerData Data;
        private readonly GameFactory _gameFactory;


        public static Game Instance { get; private set; }

        public Game(ICoroutineRuner coroutineRuner, GameObject playerPrefab, GameObject chunkGeneratorPrefab, GameObject hudPrefab)
        {
            _coroutineRuner = coroutineRuner;
            _playerPrefab = playerPrefab;
            _chunkGeneratorPrefab = chunkGeneratorPrefab;
            _hudPrefab = hudPrefab;

            Instance = this;
            Data = new PlayerData();

            _sceneLoader = new SceneLoader(_coroutineRuner);
            _gameFactory = new GameFactory(_playerPrefab, _chunkGeneratorPrefab, _hudPrefab);
            SaveLoadProgressService = new SaveLoadProgressService(_gameFactory);

            _sceneLoader.LoadInitialScene(GameLoop);
            // Здесь же выход в меню, загрузка данных и пр может быть

        }

        private void GameLoop()
        {
            // Здесь происходит основное действие игры
            // Initialize Objects

            _gameFactory.CreateHud();
            SaveLoadProgressService.LoadProgress();

            ChunkGenerator chunkGenerator = _gameFactory.CreateChunkGenerator()
                .GetComponent<ChunkGenerator>();
            var hero = _gameFactory.CreateHero(new Vector3(0f, 0.07f, 23f));

            Camera.main.GetComponent<CameraFollow>().SetTarget(hero.transform);

            MoneyTextView moneyTextView = FindMoneyTextView();
            chunkGenerator.Initialize(moneyTextView);


            // Pause if

            // Continue if

            // Exit game

            // Ressurect player

            // Stop player because of death
        }

        private MoneyTextView FindMoneyTextView()
        {
            foreach (var saved in _gameFactory.SavedObjects)
                if (saved is MoneyTextView) 
                    return saved as MoneyTextView;

            return null;
        }
    }
}
