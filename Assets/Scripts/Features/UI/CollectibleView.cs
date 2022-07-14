using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    
    private int _currentCount;

    public void Setup(int startCount, Sprite sprite)
    {
        _image.sprite = sprite;
        _currentCount = startCount;
        _text.SetText(_currentCount.ToString());
    }

    public void SetCollectiblesCount(int collectiblesCount)
    {
        _currentCount = collectiblesCount;
        _text.SetText(_currentCount.ToString());
    }
}
