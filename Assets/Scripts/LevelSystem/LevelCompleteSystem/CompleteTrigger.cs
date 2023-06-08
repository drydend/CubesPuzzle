using UnityEngine;
using WallsSystem;

namespace LevelSystem
{
    public class CompleteTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameObject _outline;

        public bool IsTriggered { get; private set; }


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out MainCube mainCube))
            {
                IsTriggered = true;
                _outline?.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MainCube mainCube))
            {
                IsTriggered = false;
                _outline?.SetActive(true);
            }
        }
    }
}