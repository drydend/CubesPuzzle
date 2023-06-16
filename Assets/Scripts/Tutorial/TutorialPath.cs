using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(menuName = "Tutorial/Tutorial path")]
    public class TutorialPath : ScriptableObject
    {
        [field: SerializeField] public TutorialStep InitialStep { get; private set; }

        private TutorialStep _currentStep;

        public TutorialStep GetCurrentStep()
        {
            if( _currentStep == null)
            {
                _currentStep = InitialStep;
                return _currentStep;
            }
            
            _currentStep = _currentStep.NextStep; 
            return _currentStep;
        }

        public void Reset()
        {
            _currentStep = null;
        }
    }
}
