using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        private GameManager _gameManager;
        private GameSettings _gameSettings;

        public float maxHealth;
        public float currentHealth;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameSettings = _gameManager.gameSettings;

            maxHealth = _gameSettings.maxHealth;
            currentHealth = _gameSettings.currentHealth;
        }

        void IDamageable.GetDamage(float damage)
        {
            float diff = currentHealth - damage;

            if (diff > 0)
            {
                Debug.Log("Decrease health");

                currentHealth = diff;


            }
            if (diff <= 0)
            {
                currentHealth = 0;

                Debug.Log("Death");
            }
        }

        private void Save()
        {
            _gameManager.SaveToJSON();
        }
    }
}