using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public interface ICoroutinePlayer
    {
        void StartRoutine(IEnumerator enumerator);
        void StopRoutine(Coroutine coroutine);
    }
}
