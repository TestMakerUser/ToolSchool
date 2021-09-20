using SmartTek.ToolSchool.Services.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmartTek.ToolSchool.Services
{
    public class SceneService : MonoBehaviour, ISceneService
    {
        public static ISceneService Instance { get; private set; }

        private SceneType? currentLoadedScene = null;
        private Coroutine coroutineLoadScene;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadScene(
            SceneType scene,
            Action onComplete)
        {
            if(coroutineLoadScene != null)
            {
                throw new InvalidOperationException($"Can't load scene {scene}. Another scene is loading now");
            }

            if(currentLoadedScene != null && currentLoadedScene == scene)
            {
                onComplete?.Invoke();
                return;
            }
            coroutineLoadScene = StartCoroutine(CoroutineLoadScene(scene, onComplete));
        }

        private IEnumerator CoroutineLoadScene(
            SceneType scene,
            Action onComplete)
        {
            if (currentLoadedScene != null)
            {
                var unloadOperation = SceneManager.UnloadSceneAsync((int)currentLoadedScene.Value);

                while (!unloadOperation.isDone)
                {
                    yield return null;
                }

                currentLoadedScene = null;
            }

            var operation = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
            operation.allowSceneActivation = true;
            while (!operation.isDone)
            {
                yield return null;
            }
            currentLoadedScene = scene;

            coroutineLoadScene = null;
            onComplete?.Invoke();
        }


        /// <summary>
        /// Scenes, which user can load during gameplay. Value is scene build index.
        /// </summary>
        public enum SceneType
        {
            Lobby = 1,
            Workshop = 2
        }
    }
}