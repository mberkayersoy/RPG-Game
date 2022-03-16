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
        [SerializeField] float weaponDamage = 5;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;
        Health target; // It used to be transformed, but we changed it to access the health class more easily. 
                       //Since every enemy has to have health, it will not cause much trouble.
        float timeSinceLastAttack = Mathf.Infinity;
        
        private void Start() 
        {
            SpawnMethod();    
        }

        private void Update() 
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;
            
            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 0.8f); // enemies chaseing speed.
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void SpawnMethod()
        {
            Instantiate(weaponPrefab, handTransform);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;
                //enemyHealth.TakeDamage(damage); enemy take damage my way

            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        //This method represents the specific moment in the attack animation
        //when the hitting action takes place between the player and the enemy. 
        //If we look at the attack animation, we can see the key of this method in about the 11th frame.
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack ();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}

