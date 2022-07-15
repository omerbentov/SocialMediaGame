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
    private Vector3 _direction;
    private float _distance;
    private Vector3 _medianWithNoise;
    private float _halfMovementTime;

    void Start()
    {
        _movementTime = Client.Client.Instance.Configuration.UserInterfaceSo.CollectibleTimerToMove;
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
        _direction = (startPosition - _goToPosition).normalized;
        _distance = Vector3.Distance(startPosition, _goToPosition);

        var medianOnLine = startPosition + ((_distance / 4) * _direction);
        var tangent = Vector3.Cross(_direction, Vector3.forward);
        if(tangent == Vector3.zero)  Vector3.Cross(_direction, Vector3.up);
        _medianWithNoise = medianOnLine + tangent.normalized *  Random.Range(-1 * _distance / 10, _distance / 10);
    }
}
