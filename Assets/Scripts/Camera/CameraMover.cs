using LevelSystem;
using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _animationTime;
    [SerializeField]
    private AnimationCurve _animationCurve;

    private LevelConfig _levelConfig;

    private Coroutine _coroutine;

    public void SetLevelConfig(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }

    public void MoveToLevelRuningPosition()
    {   
        if(_coroutine != null ) 
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(MoveToLevelRuningPositionRoutine());
    }

    public void SetToLevelStartPosition()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _camera.transform.position = _levelConfig.InitialCameraPosition;
    }

    private IEnumerator MoveToLevelRuningPositionRoutine()
    {
        var timeElapsedNormalized = 0f;

        while (timeElapsedNormalized != 1)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, 
                _levelConfig.CameraPosition, _animationCurve.Evaluate(timeElapsedNormalized));

            timeElapsedNormalized = Mathf.Clamp(timeElapsedNormalized + Time.deltaTime / _animationTime, 0 , 1);
            yield return null;
        }
    }
}