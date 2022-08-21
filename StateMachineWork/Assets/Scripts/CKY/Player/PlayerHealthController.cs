using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        public float maxHealth = 100;
        public float currentHealth;

        private void Start()
        {
            Debug.Log("TODO: Get health values from saved scriptable. But right now;");
            currentHealth = maxHealth;
        }

        void IDamageable.GetDamage(float damage)
        {
            float diff = currentHealth - damage;

            if (diff > 0)
            {
                Debug.Log("Decrease health");
            }
            if (diff <= 0)
            {
                currentHealth = 0;

                Debug.Log("Death");
            }
        }
    }
}