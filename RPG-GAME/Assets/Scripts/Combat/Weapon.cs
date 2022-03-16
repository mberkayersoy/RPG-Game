using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]

    
    public class Weapon : ScriptableObject 
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float weaponDamage = 5;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Debug.Log("Spawn ilk if");
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                Debug.Log("Spawn ikinci if");
                animator.runtimeAnimatorController = animatorOverride;
            }
            
        }

        public float GetRange()
        {
            return weaponRange;
        }
        
        public float GetDamage()
        {
            return weaponDamage;
        }
    }  
} 