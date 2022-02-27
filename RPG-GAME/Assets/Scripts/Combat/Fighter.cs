using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float timeBetweenAttacks = 1f;
        //[SerializeField] Health enemyHealth; enemy take damage my way.
        [SerializeField] float weaponDamage = 5;
        Health target; // It used to be transformed, but we changed it to access the health class more easily. 
                          //Since every enemy has to have health, it will not cause much trouble.
        float timeSinceLastAttack = 0;
        
        private void Update() 
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;
            
            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position );
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack > timeBetweenAttacks) 
            {
                //This will trigger the Hit() event.
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
                //enemyHealth.TakeDamage(damage); enemy take damage my way
            
            }
        }

        //Animation Event
        //This method represents the specific moment in the attack animation
        //when the hitting action takes place between the player and the enemy. 
        //If we look at the attack animation, we can see the key of this method in about the 11th frame.
        void Hit()
        {
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack"); 
            target = null;
        }
    }
}

