using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class FeedView : MonoBehaviour
{
    private const int BETWEEN_ITEMS = 200;
    private const int START_Y_OFFSET = -100;

    private bool _ended;
    private FeedSO _feedSO;
    private float _startTime;
    private Sprite[] _badSprites;
    private Sprite[] _goodSprites;
    private ScrollRect _scrollRect;
    private GameObject _feedItemPrefab;
    private GameObject _collectibleWidgetPrefab;

    [SerializeField] public ConvexHullParent _contentParent;

    private Dictionary<eCollectibleType, Transform> _collectiblesLocationDictionary;

    public Transform GetCollectibleGotoLocation(eCollectibleType type)
    {
        return _collectiblesLocationDictionary.FirstOrDefault(c => c.Key == type).Value;
    }

    public event Action ScrollEndedEvent;

    public bool EnableAutoScrolling { get; set; }

    public void Setup(FeedSO feedSo)
    {
        _feedSO = feedSo;
        _feedItemPrefab = Client.Client.Instance.Configuration.FeedItemPrefab;
        _collectibleWidgetPrefab = Client.Client.Instance.Configuration.CollectibleWidgetPrefab;
        _scrollRect = GetComponent<ScrollRect>();

        var itemLength = _feedItemPrefab.GetComponent<RectTransform>().rect.height;
        var addedOffset = new Vector3(0,  - BETWEEN_ITEMS - itemLength, 0);
        var startVectorOffset = new Vector3(0, START_Y_OFFSET, 0);

        SetupContentSize(feedSo);
        CreateItems(feedSo.Shuffled, startVectorOffset, addedOffset);
    }

    private void SetupContentSize(FeedSO feedSo)
    {
        _contentParent.GetComponent<RectTransform>().sizeDelta =
            new Vector2(
                0,
                _feedItemPrefab.GetComponent<RectTransform>().rect.height * feedSo.ItemCount + (feedSo.ItemCount - 1) * BETWEEN_ITEMS);
    }

    private void CreateItems(FeedItemSO[] feedItemsData, Vector3 accumulatedOffset, Vector3 addedOffset)
    {
        if (feedItemsData == null) return;
        
        foreach (var feedItemData in feedItemsData)
        {
            var item = Instantiate(_feedItemPrefab, _contentParent.transform).GetComponent<FeedItemView>();
            item.Setup(feedItemData, accumulatedOffset);
            accumulatedOffset += addedOffset;
        }
        
        _contentParent.Resize();
    }

    void Update()
    {
        AutoScrolling();
    }

    private void AutoScrolling()
    {
        if (!EnableAutoScrolling) return;

        _startTime += Time.fixedDeltaTime;
        var scrollPosition = _startTime / _feedSO.ScrollTimeInSecounds;

        if (scrollPosition > 1)
        {
            if (!_ended)
            {
                _ended = true;
                ScrollEndedEvent?.Invoke();
            }
        }
        else
        {
            _scrollRect.verticalNormalizedPosition = Mathf.Lerp(1, 0, scrollPosition);
        }

    }
}

internal class FeedScrollEndedEvent
{
}
