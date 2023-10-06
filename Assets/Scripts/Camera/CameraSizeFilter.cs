using LevelSystem;
using System.Collections;
using UnityEngine;

public class CameraSizeFilter : MonoBehaviour
{
    private const float OriginScreenRation = 16/9;

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

        _camera.orthographicSize = _levelConfig.InitialCameraSize;

    }

    private IEnumerator MoveToLevelRuningPositionRoutine()
    {
        var timeElapsedNormalized = 0f;

        while (timeElapsedNormalized != 1)
        {   
            var cameraSize = Mathf.Lerp(_levelConfig.InitialCameraSize, _levelConfig.CameraSize, timeElapsedNormalized);
            _camera.orthographicSize = cameraSize;

            timeElapsedNormalized = Mathf.Clamp(timeElapsedNormalized + Time.deltaTime / _animationTime, 0 , 1);
            yield return null;
        }
    }
}