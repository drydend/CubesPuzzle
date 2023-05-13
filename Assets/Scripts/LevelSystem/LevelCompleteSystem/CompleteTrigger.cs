using UnityEngine;
using WallsSystem;

namespace LevelSystem
{

    public class CompleteTrigger : MonoBehaviour
    {   
        public bool IsTriggered { get; private set; }


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out MainCube mainCube))
            {
                IsTriggered = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MainCube mainCube))
            {
                IsTriggered = false;
            }
        }
    }
}