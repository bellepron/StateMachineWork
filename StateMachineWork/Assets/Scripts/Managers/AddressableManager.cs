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
        [SerializeField] private AssetReference[] addressableLevels;
        public Transform loadedLevel;
        private bool _isLevelReady;

        [Header("Characters")]
        [SerializeField] private AssetReference[] characters;
        public Transform loadedCharacter;
        private bool _isCharacterReady;
        [SerializeField] Transform playerHolderTr;

        [Header("Weapons")]
        [SerializeField] private AssetReference[] weapons;
        public Transform loadedWeapon;
        private bool _isWeaponReady;
        private Transform _weaponHolderTr;

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
                if (_isCharacterReady == false) yield return null;
                if (_isCharacterReady == false) yield return null;
                if (_isMatReady0 == false) yield return null;
                if (_isBullet0Ready == false) yield return null;
                if (_isWeaponReady == false) yield return null;

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
                //Debug.Log("Level loaded");
            };
        }

        private void LoadPlayer()
        {
            characters[0].InstantiateAsync().Completed += (character) =>
            {
                this.loadedCharacter = character.Result.transform;

                if (playerHolderTr == null) playerHolderTr = FindObjectOfType<Player.PlayerHolder>().transform;
                this.loadedCharacter.parent = playerHolderTr;
                this.loadedCharacter.localPosition = Vector3.zero;
                this.loadedCharacter.localRotation = Quaternion.Euler(0, 90, 0);

                _isCharacterReady = true;
                //Debug.Log("Player loaded");

                _weaponHolderTr = this.loadedCharacter.GetComponentInChildren<Player.WeaponHolder>().transform;

                LoadWeapon();
            };
        }

        private void LoadWeapon()
        {
            weapons[0].InstantiateAsync().Completed += (weapon) =>
            {
                this.loadedWeapon = weapon.Result.transform;
                this.loadedWeapon.parent = _weaponHolderTr;
                this.loadedWeapon.localPosition = Vector3.zero;
                this.loadedWeapon.localRotation = Quaternion.Euler(0, 0, 0);

                _isWeaponReady = true;
                //Debug.Log("Weapon loaded");

                _gameEvents.WeaponLoadedEvent(this.loadedWeapon.GetComponent<AbstractWeapon>());
            };
        }

        private void LoadMaterial0()
        {
            matR0.LoadAssetAsync().Completed += (mat) =>
            {
                this.mat0 = mat.Result;
                _isMatReady0 = true;
                //Debug.Log("ball material loaded");
            };
        }

        private void LoadBullet0()
        {
            bulletR0.LoadAssetAsync<GameObject>().Completed += (bullet) =>
            {
                this.bullet0 = bullet.Result.transform;
                _isBullet0Ready = true;
                //Debug.Log("bullet0 loaded");

                BulletSpawner.Instance.bulletPrefabTr = this.bullet0;
            };
        }
    }
}