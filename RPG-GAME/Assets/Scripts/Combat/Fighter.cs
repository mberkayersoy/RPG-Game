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
        Transform target;
        float timeSinceLastAttack = 0;
        
        private void Update() 
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            
            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position );
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
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);            
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}

