using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public interface ICoroutinePlayer
    {
        Coroutine StartRoutine(IEnumerator enumerator);
        void StopRoutine(Coroutine coroutine);
    }
}
