using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FeedItemView : MonoBehaviour
{
    [SerializeField] private bool _isGood;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI Title; 
    [SerializeField] private TextMeshProUGUI Likes; 

    public void Setup(FeedItemSO feedItemSoData, Vector3 accumulatedOffset)
    {
        _isGood = feedItemSoData.IsGood;
        transform.position += accumulatedOffset;
        _image.sprite = feedItemSoData.Sprite;
        Title.SetText(feedItemSoData.Title);
        Likes.SetText(feedItemSoData.Likes.ToString());
    }

    public void OnClick()
    {
        Client.Client.Instance.Broadcaster.Broadcast(
            new ItemClickedEvent()
            {
                IsGood = _isGood,
                Position = transform.position
            });
    }
}

public class ItemClickedEvent
{
    public bool IsGood { get; set; }
    public Vector3 Position { get; set; }
}
