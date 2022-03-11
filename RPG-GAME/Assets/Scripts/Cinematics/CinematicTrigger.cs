using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour 
    {
        bool triggerCount = false;
        
        private void OnTriggerEnter(Collider other) {
            
            if (other.gameObject.tag == "Player" && triggerCount == false)
            {
                triggerCount = true;
                GetComponent<PlayableDirector>().Play();
            }

        }
    }
}
