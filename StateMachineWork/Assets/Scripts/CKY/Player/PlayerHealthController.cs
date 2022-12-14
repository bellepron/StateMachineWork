using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        private GameManager _gameManager;
        private GameSettings _gameSettings;
        private RagdollToggle _ragdollToggle;
        private Rigidbody _rb;
        private FSM.StateMachinePlayer _smPlayer;
        private GameEvents _gameEvents;

        public float maxHealth;
        public float currentHealth;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameSettings = _gameManager.gameSettings;
            _ragdollToggle = GetComponent<RagdollToggle>();
            _rb = GetComponent<Rigidbody>();
            _smPlayer = GetComponent<FSM.StateMachinePlayer>();
            _gameEvents = FindObjectOfType<GameEvents>();

            maxHealth = _gameSettings.maxHealth;
            currentHealth = _gameSettings.currentHealth;

            GameEvents.GameRestart += GameRestarted;
        }

        private void GameRestarted()
        {
            currentHealth = maxHealth;
        }

        void IDamageable.GetDamage(float damage, Transform fromWhat)
        {
            float diff = currentHealth - damage;

            if (diff > 0)
            {
                currentHealth = diff;

                Vector3 direction = (transform.position - fromWhat.position).normalized; // TODO: Move state overrides this.
                _rb.AddForce(direction * 200, ForceMode.Impulse);

                Save();
            }
            if (diff <= 0)
            {
                currentHealth = 0;

                _smPlayer.Death();
                _ragdollToggle.RagdollActivate(true);
                Vector3 direction = (transform.position - fromWhat.position).normalized;
                _ragdollToggle.AddForceToPelvis(direction * 200);

                _gameEvents.GameFailEvent();
            }

            CameraManager.Instance.Shake(0.5f, 0.5f, 0.2f);
        }

        private void Save()
        {
            _gameManager.SaveToJSON();
        }
    }
}