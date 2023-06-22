using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Subway.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRuner _coroutineRuner;

        private const string InitialLevelName = "Game";

        public SceneLoader(ICoroutineRuner coroutineRuner)
        {
            _coroutineRuner = coroutineRuner;
        }

        public void LoadInitialScene(Action onLoaded) => LoadScene(InitialLevelName, onLoaded);

        public void LoadScene(string name, Action onLoaded) 
            => _coroutineRuner.StartCoroutine(LoadSceneRoutine(name, onLoaded));

        IEnumerator LoadSceneRoutine(string sceneName, Action onLoaded)
        {
            if (sceneName == SceneManager.GetActiveScene().name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation sceneWaiterAsync = SceneManager.LoadSceneAsync(sceneName);

            if (sceneWaiterAsync.isDone == false)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
