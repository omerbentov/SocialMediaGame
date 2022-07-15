using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScaleUpDown))]
public class CollectibleWidgetView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    
    private int _currentCount;
    private ScaleUpDown _scaleUpDown;

    public void Setup(int startCount, Sprite sprite)
    {
        _scaleUpDown = GetComponent<ScaleUpDown>();
        _image.sprite = sprite;
        _currentCount = startCount;
        _text.SetText(_currentCount.ToString());
    }

    public void SetCollectiblesCount(int collectiblesCount)
    {
        if (collectiblesCount > _currentCount && !_scaleUpDown.enabled)
        {
            _scaleUpDown.enabled = true;
        }
        
        _currentCount = collectiblesCount;
        _text.SetText(_currentCount.ToString());
    }
}
