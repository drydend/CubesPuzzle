using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    public class PointsMenu : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _menuDirection;

        [SerializeField]
        private RectTransform _parent;
        [SerializeField]
        private int _distanceBetweenElements;

        [SerializeField]
        private PointsMenuPoint _mainPointPrefab;
        [SerializeField]
        private PointsMenuBackgroundPoint _backGroundPointPrefab;

        private PointsMenuPoint _mainPoint;
        private List<PointsMenuBackgroundPoint> _points = new List<PointsMenuBackgroundPoint>();
        private int _mainPointCurrentPosition;

        public void Initialize(int numberOfPoints, int initialPosition)
        {
            if (numberOfPoints % 2 == 0)
            {
                InitWithEvenNumberOfPoints(numberOfPoints);
            }
            else
            {
                InitWithOddNumberOfPoints(numberOfPoints);
            }

            _mainPoint = Instantiate(_mainPointPrefab, _parent);
            var mainPointPosition = _points[initialPosition].RectTransform.anchoredPosition;
            _mainPoint.Initialize(mainPointPosition);
            _mainPointCurrentPosition = initialPosition;
        }

        private void InitWithOddNumberOfPoints(int numberOfPoints)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                var pointPosition = GetPositionForOddNumberOfPoints(numberOfPoints, i);
                var position = Vector2.zero + _menuDirection * _distanceBetweenElements * pointPosition;

                var pointBackground = Instantiate(_backGroundPointPrefab, _parent);
                pointBackground.RectTransform.anchoredPosition = position;
                _points.Add(pointBackground);
            }
        }

        private void InitWithEvenNumberOfPoints(int numberOfPoints)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                var pointPosition = GetPositionForEvenNumberOfPoints(numberOfPoints, i);
                var position = Vector2.zero + _menuDirection * _distanceBetweenElements * pointPosition;

                var pointBackground = Instantiate(_backGroundPointPrefab, _parent);
                pointBackground.RectTransform.anchoredPosition = position;
                _points.Add(pointBackground);
            }
        }

        public bool TryMoveTo(MoveDirection direction, float time)
        {
            if (MoveDirection.Forward == direction)
            {
                return TryMoveForward(time);
            }
            else
            {
                return TryMoveBackwards(time);
            }
        }

        private bool TryMoveBackwards(float time)
        {
            if (_mainPointCurrentPosition == 0)
            {
                return false;
            }

            _mainPointCurrentPosition--;
            _mainPoint.MoveTo(_points[_mainPointCurrentPosition].RectTransform.anchoredPosition, time);
            return true;
        }

        private bool TryMoveForward(float time)
        {
            if (_mainPointCurrentPosition == _points.Count - 1)
            {
                return false;
            }

            _mainPointCurrentPosition++;
            _mainPoint.MoveTo(_points[_mainPointCurrentPosition].RectTransform.anchoredPosition, time);
            return true;
        }

        private float GetPositionForEvenNumberOfPoints(int numberOfPoints, int i)
        {
            return (float)i - ((float)numberOfPoints / 2 - 0.5f);
        }

        private float GetPositionForOddNumberOfPoints(int numberOfPoints, int i)
        {
            return (float)i - ((float)numberOfPoints / 2 - 0.5f);
        }
    }
}
