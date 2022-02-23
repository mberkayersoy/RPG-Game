using RPG.Movement;
using UnityEngine;

namespace RPG.Control 
{
    
    public class PlayerControllar : MonoBehaviour
    {
        private void Update() 
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }    
        }
        private void MoveToCursor() 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool Hashit = Physics.Raycast(ray, out hit);
            if (Hashit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}

