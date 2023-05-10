using System.Collections;
using UnityEngine;

namespace Utils
{
    public class CoroutinePlayer : MonoBehaviour, ICoroutinePlayer
    {
        public Coroutine StartRoutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void StopRoutine(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}