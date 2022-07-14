using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FeedItemController : MonoBehaviour
{
    [SerializeField] private bool _isGood;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI Title; 
    [SerializeField] private TextMeshProUGUI Likes; 

    public void Setup(FeedItem feedItemData, Vector3 accumulatedOffset)
    {
        _isGood = feedItemData.IsGood;
        transform.position += accumulatedOffset;
        _image.sprite = feedItemData.Sprite;
        Title.SetText(feedItemData.Title);
        Likes.SetText(feedItemData.Likes.ToString());
    }

    public void OnClick()
    {
        Debug.Log(_isGood);
    }
}
