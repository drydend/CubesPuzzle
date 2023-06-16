using GameUI;
using Input;
using LevelSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tutorial
{
    public class TutorialVisualizer : MonoBehaviour
    {
        private readonly Vector3 AboveOffset = new Vector3(0, 1.5f, 0);

        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private TMP_Text _hintText;
        [SerializeField]
        private UIMenu _tutorialMenu;
        [SerializeField]
        private float _textWritingSpeed;

        private LevelPreset _levelPreset;

        private TutorialStep _currentStep;

        private Coroutine _textWritingRoutine;
        private List<GameObject> _visualObjects = new List<GameObject>();

        private bool _isMenuOpened;
        public bool IsAnimationOver { get; private set; }

        public void Initialize(LevelPreset levelPreset)
        {
            _levelPreset = levelPreset;
        }

        public void Show()
        {
            if (!_isMenuOpened)
            {
                _tutorialMenu.Open();
                _isMenuOpened = true;
            }

        }

        public void Hide()
        {
            if (_isMenuOpened)
            {
                _tutorialMenu.Close();
                _isMenuOpened = false;
            }
        }

        public void VisualizeStep(TutorialStep step)
        {
            _currentStep = step;
            IsAnimationOver = false;

            CloseCurrentStep();

            CreateVisualObjects(step);
            _hintText.text = step.HintText;
            _textWritingRoutine = StartCoroutine(WriteStepTextRoutine(step));
        }

        public void SkipAnimation()
        {
            if (_textWritingRoutine != null)
            {
                StopCoroutine(_textWritingRoutine);
            }

            IsAnimationOver = true;
            _text.text = _currentStep.StepText;
        }

        public void CloseCurrentStep()
        {
            if (_textWritingRoutine != null)
            {
                StopCoroutine(_textWritingRoutine);
            }

            foreach (var obj in _visualObjects)
            {
                Destroy(obj);
            }

            _text.text = string.Empty;
            _hintText.text = string.Empty;
            _visualObjects.Clear();
        }

        private void CreateVisualObjects(TutorialStep step)
        {
            foreach (var visualObject in step.VisualObjects)
            {
                var positions = GetObjectsPositions(visualObject);

                foreach (var pos in positions)
                {
                    var instance = Instantiate(visualObject.ObjectPrefab, pos, Quaternion.identity);
                    _visualObjects.Add(instance);
                }
            }
        }

        private List<Vector3> GetObjectsPositions(TutorialVisualObject prefab)
        {
            switch (prefab.Position)
            {
                case TutorialVisualObjectPosition.AboveMainCube:
                    return GetPositionAboveMainCubes();
                case TutorialVisualObjectPosition.AboveCompleteTrigger:
                    return GetPositionAboveCompleteTriggers();
                default:
                    throw new NotImplementedException();
            }
        }

        private List<Vector3> GetPositionAboveCompleteTriggers()
        {
            var result = new List<Vector3>();

            foreach (var completeTrigger in _levelPreset.CompleteTriggers)
            {
                result.Add(completeTrigger.transform.position + AboveOffset);
            }

            return result;
        }

        private List<Vector3> GetPositionAboveMainCubes()
        {
            var result = new List<Vector3>();

            foreach (var mainCube in _levelPreset.MainCubes)
            {
                result.Add(mainCube.transform.position + AboveOffset);
            }

            return result;
        }

        private IEnumerator WriteStepTextRoutine(TutorialStep step)
        {
            for (int i = 0; i < step.StepText.Length; i++)
            {
                while (step.StepText[i] == '<')
                {
                    var tag = GetTagAt(i, step.StepText, out int tagEndPos);
                    _text.text += tag;
                    i = tagEndPos;

                    if (i + 1 >= step.StepText.Length)
                    {
                        break;
                    }
                }

                _text.text += step.StepText[i];

                yield return new WaitForSeconds(1f / _textWritingSpeed);
            }

            IsAnimationOver = true;
        }

        private string GetTagAt(int position, string text, out int tagEndPos)
        {
            for (int i = position; i < text.Length; i++)
            {
                if (text[i] == '>')
                {
                    tagEndPos = i;
                    return text.Substring(position, i - position);
                }
            }

            tagEndPos = position;
            return text[position].ToString();
        }
    }
}
