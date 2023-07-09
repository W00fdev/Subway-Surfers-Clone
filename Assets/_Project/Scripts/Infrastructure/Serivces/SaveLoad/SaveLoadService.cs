using System;
using System.Collections.Generic;
using UnityEngine;
using Subway.Logic.Data;

namespace Subway.Infrastructure.Serivces.SaveLoad
{
    public class SaveLoadProgressService : ISaveLoadProgressService
    {
        private const string SaveProgressKey = "Progress";
        
        private readonly GameFactory _gameFactory;

        public SaveLoadProgressService(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (var saved in _gameFactory.SavedObjects)
                saved.Save();

            var savedProgress = JsonUtility.ToJson(Game.Instance.Data);

            Debug.Log("SaveProgress adress: " + Game.Instance.Data.Money);

            PlayerPrefs.SetString(SaveProgressKey, savedProgress);

            Debug.Log("Progress saved");

            Debug.Log("JSON: " + savedProgress);
        }

        public void LoadProgress()
        {
            if (PlayerPrefs.HasKey(SaveProgressKey) == false)
            {
                Debug.Log("false load");
                return;
            }

            var savedString = PlayerPrefs.GetString(SaveProgressKey);
            var playerData = JsonUtility.FromJson<PlayerData>(savedString);

            Debug.Log("loader from prefs: " + playerData);

            Game.Instance.Data.Money = playerData.Money;
            Game.Instance.Data.Score = playerData.Score;

           // PlayerData.Instance.Money = 1;

           // Debug.Log("Money from PlayerData: " + PlayerData.Instance.Money);

         //   Debug.Log("Progress loaded");


            foreach (var saved in _gameFactory.SavedObjects)
                if (saved is ISavedLoadLogic)
                    (saved as ISavedLoadLogic).Load();
        }
    }
}
