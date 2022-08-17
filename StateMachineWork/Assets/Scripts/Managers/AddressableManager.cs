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
    public class AddressableManager : MonoBehaviour
    {
        private GameEvents _gameEvents;

        [Header("Levels")]
        [SerializeField] private List<AssetReference> addressableLevels;
        public Transform loadedLevel;
        private bool _isLevelReady;

        [Header("Player")]
        [SerializeField] private AssetReference playerR;
        public Transform loadedPlayer;
        private bool _isPlayerReady;

        [Header("Materials")]
        [SerializeField] private AssetReferenceMaterial matR0;
        public Material mat0;
        private bool _isMatReady0;

        [Header("Bullet")]
        [SerializeField] private AssetReference bulletR0;
        public Transform bullet0;
        private bool _isBullet0Ready;

        private void Start()
        {
            _gameEvents = FindObjectOfType<GameEvents>();

            Addressables.InitializeAsync().Completed += Load;

            StartCoroutine(Control());
        }
        IEnumerator Control()
        {
            bool isUpdating = true;
            while (isUpdating == true)
            {
                if (_isLevelReady == false) yield return null;
                Debug.Log("Level loaded");
                if (_isPlayerReady == false) yield return null;
                Debug.Log("Player loaded");
                if (_isMatReady0 == false) yield return null;
                Debug.Log("ball material loaded");
                if (_isBullet0Ready == false) yield return null;
                Debug.Log("bullet0 loaded");

                _gameEvents.AddressablesLoadedEvent();
                Debug.Log("All addressables Loaded!");

                isUpdating = false;

                yield return null;
            }
        }

        private void Load(AsyncOperationHandle<IResourceLocator> obj)
        {
            LoadLevel();
            LoadPlayer();
            LoadMaterial0();
            LoadBullet0();
        }

        private void LoadLevel()
        {
            //var levelToLoad = addressableLevels[levelIndex % addressableLevels.Count];
            var levelToLoad = addressableLevels[0];
            levelToLoad.InstantiateAsync().Completed += (level) =>
            {
                loadedLevel = level.Result.transform;
                _isLevelReady = true;
            };
        }

        private void LoadPlayer()
        {
            playerR.InstantiateAsync().Completed += (bullet) =>
            {
                this.loadedPlayer = bullet.Result.transform;
                this.loadedPlayer.position = Vector3.zero;
                _isPlayerReady = true;

                
            };
        }

        private void LoadMaterial0()
        {
            matR0.LoadAssetAsync().Completed += (mat) =>
            {
                this.mat0 = mat.Result;
                _isMatReady0 = true;
            };
        }

        private void LoadBullet0()
        {
            bulletR0.LoadAssetAsync<GameObject>().Completed += (bullet) =>
            {
                this.bullet0 = bullet.Result.transform;
                _isBullet0Ready = true;

                BulletSpawner.Instance.bulletPrefabTr = this.bullet0;
            };
        }
    }
}