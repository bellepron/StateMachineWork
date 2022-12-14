using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CKY.AI
{
    public class AIHealthController : MonoBehaviour, IDamageable
    {
        private GameManager _gameManager;
        private GameSettings _gameSettings;
        private RagdollToggle _ragdollToggle;
        private Rigidbody _rb;
        private FSM.StateMachineAI _smAI;

        public float maxHealth;
        public float currentHealth;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameSettings = _gameManager.gameSettings;
            _ragdollToggle = GetComponent<RagdollToggle>();
            _rb = GetComponent<Rigidbody>();
            _smAI = GetComponent<FSM.StateMachineAI>();

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

                _smAI.Death();
                _ragdollToggle.RagdollActivate(true);
                Vector3 direction = (transform.position - fromWhat.position).normalized;
                _ragdollToggle.AddForceToPelvis(direction * 200);
            }

            CameraManager.Instance.Shake(0.2f, 0.2f, 0.1f);
        }

        private void Save()
        {
            _gameManager.SaveToJSON();
        }
    }
}