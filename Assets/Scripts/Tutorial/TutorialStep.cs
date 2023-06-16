using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(menuName = @"Tutorial/TutorialStep")]
    public class TutorialStep : ScriptableObject
    {
        [field: SerializeField] [field: TextArea] public string StepText { get; private set; }
        [field: SerializeField] [field: TextArea] public string HintText { get; private set; }
        [field: SerializeField] public TutorialStep NextStep { get; private set; }
        [field: SerializeField] public TutorialStepCompleteCodition CompleteCodition { get; private set; }
        [field: SerializeField] public List<TutorialVisualObject> VisualObjects { get; private set; }
    }
}
