using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAferEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;
        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else{
                    Destroy(gameObject);
                }
            }    
        }
    }
}