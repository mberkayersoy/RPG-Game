using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour 
    {
        [SerializeField] float experiencePoints = 10;

        public void GameExperience(float experience)
        {
            experiencePoints += experience;
        }
    }
}