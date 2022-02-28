using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control 
{
    
    public class PlayerControllar : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit [] hits = Physics.RaycastAll(GetMouseRay()); 
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                //if (target == null) continue;  -> old code. Because Fighter.cs CanAttack().
                if (!GetComponent<Fighter>().CanAttack(target)) // new code.
                {
                    continue;
                }  
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool Hashit = Physics.Raycast(GetMouseRay(), out hit);
            if (Hashit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

