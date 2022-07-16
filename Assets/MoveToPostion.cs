using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveToPostion : MonoBehaviour
{
    private float _movementTime;
    private Vector3 _goToPosition;
    private Vector3 _basePosition;
    private float _timeSinceEnabled;
    private Action _onArrival;
    private Vector3 _medianWithNoise;
    private float _halfMovementTime;

    void Start()
    {
        _movementTime = Client.Client.Instance.Configuration.CollectibleTimerToMove;
        _goToPosition = Client.Client.Instance.UI.CollectibleTransform.position;
    }
    
    void Update()
    {
        _timeSinceEnabled += Time.fixedDeltaTime;

        _halfMovementTime = _movementTime / 2;

        if (_timeSinceEnabled < _halfMovementTime)
        {
            transform.position = Vector3.Lerp(_basePosition, _medianWithNoise, _timeSinceEnabled / (_halfMovementTime));
        }
        else
        {
            transform.position = Vector3.Lerp(_medianWithNoise, _goToPosition, (_timeSinceEnabled - (_halfMovementTime)) / (_halfMovementTime));
            
            if (_timeSinceEnabled > _movementTime)
            {
                _onArrival.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    public void Setup(Vector3 startPosition, Action onArrival)
    {
        _basePosition = startPosition;
        transform.position = _basePosition;
        _onArrival = onArrival;
        
        var directionNormalized = (startPosition - _goToPosition).normalized;
        var distance = Vector3.Distance(startPosition, _goToPosition);
        var medianOnLine = startPosition + ((distance / 3f) * directionNormalized);
        var tangentNormalized = Vector3.Cross(directionNormalized, Vector3.forward).normalized;
        if(tangentNormalized == Vector3.zero)  tangentNormalized = Vector3.Cross(directionNormalized, Vector3.up).normalized;
        var random = Random.Range(-1 * distance / 10, distance / 10);
        
        _medianWithNoise = medianOnLine + (tangentNormalized * random);
    }
}
