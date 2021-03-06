using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;
using RPG.Core;
using RPG.Combat;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float regenerationPercentage = 70;
        float healthPoints = -1f;
        bool isDead = false;

        Collider capsuleCollider;
        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenarateHealth;
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }

            capsuleCollider = GetComponent<Collider>();
        }

        public bool IsDead()
        {
            if (isDead == true) capsuleCollider.enabled = false;
            return isDead;
        }

        public void TakeDamage(GameObject insigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            

            if (healthPoints == 0)
            {
                Die();
                AwardExperience(insigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if(isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject insigator)
        {
            Experience experience = insigator.GetComponent<Experience>();

            if (experience == null) return;

            experience.GameExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenarateHealth()
        {
            float regenHealthPoints= GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }
    }
}
