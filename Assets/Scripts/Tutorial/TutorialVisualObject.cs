using System;
using UnityEngine;

namespace Tutorial
{
    [Serializable]
    public class TutorialVisualObject 
    {
        [field: SerializeField] public GameObject ObjectPrefab { get; private set; }
        [field: SerializeField] public TutorialVisualObjectPosition Position { get; private set; }
    }
}
