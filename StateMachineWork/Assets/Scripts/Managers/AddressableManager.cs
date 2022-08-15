using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UniRx;

namespace CKY.MANAGERS
{
    public class AddressableManager : Singleton<AddressableManager>
    {
        private GameEvents _gameEvents;

        [Header("Materials")]
        [SerializeField] private AssetReferenceMaterial matR0;
        public Material mat0;
        private bool _isMatReady0;

        [Header("Levels")]
        [SerializeField] private List<AssetReference> addressableLevels;
        private bool _isLevelReady;

        private void Start()
        {
            _gameEvents = FindObjectOfType<GameEvents>();

            Addressables.InitializeAsync().Completed += GetMaterial;
            Addressables.InitializeAsync().Completed += GetLevel;

            StartCoroutine(Control());
        }
        IEnumerator Control()
        {
            bool isUpdating = true;
            while (isUpdating == true)
            {
                if (_isLevelReady == false) yield break;
                if (_isMatReady0 == false) yield break;

                _gameEvents.AddressablesLoadedEvent();
                Debug.Log("Addressables Loaded!");

                isUpdating = false;

                yield return null;
            }
        }

        private void GetLevel(AsyncOperationHandle<IResourceLocator> obj)
        {
            //var levelToLoad = addressableLevels[levelIndex % addressableLevels.Count];
            var levelToLoad = addressableLevels[0];
            levelToLoad.InstantiateAsync().Completed += (level) =>
            {
                var loadedLevel = (GameObject)level.Result;
                _isLevelReady = true;
            };
        }

        private void GetMaterial(AsyncOperationHandle<IResourceLocator> obj)
        {
            matR0.LoadAssetAsync().Completed += (mat) =>
            {
                this.mat0 = mat.Result;
                _isMatReady0 = true;
            };
        }
    }
}