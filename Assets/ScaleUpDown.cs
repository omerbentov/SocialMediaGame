using UnityEngine;

public class ScaleUpDown : MonoBehaviour
{
    [SerializeField] private float _scaleTime = 1.5f;
    
    private Vector3 _baseScale;
    private Vector3 _goToScale;
    private float _timeSinceEnabled;

    private float ScaleHalfTime
    {
        get { return _scaleTime / 2; }
    }

    private void Start()
    {
        _baseScale = transform.localScale;
        _goToScale = transform.localScale * 1.5f;
    }

    void OnEnable()
    {
        _timeSinceEnabled = 0;
    }

    void FixedUpdate()
    {
        _timeSinceEnabled += Time.fixedDeltaTime;

        if (_timeSinceEnabled < ScaleHalfTime)
        {
            transform.localScale = Vector3.Lerp(_baseScale, _goToScale, _timeSinceEnabled / ScaleHalfTime);
        }
        else
        {
            transform.localScale = 
                Vector3.Lerp(_goToScale, _baseScale, (_timeSinceEnabled - ScaleHalfTime) / ScaleHalfTime);

            if (_timeSinceEnabled > _scaleTime)
            {
                enabled = false;
            }
        }
    }
}
