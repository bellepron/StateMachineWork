using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] Animator anim;
        [SerializeField] Transform pelvisTr;

        private void Start()
        {
            RagdollToggle ragdollToggle = transform.parent.GetComponent<RagdollToggle>();
            ragdollToggle.animator = anim;
            ragdollToggle.pelvisTr = pelvisTr;
            ragdollToggle.Initialize();
        }
    }
}