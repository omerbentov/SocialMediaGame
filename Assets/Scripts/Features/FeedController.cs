using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class FeedController : MonoBehaviour
{
    private const string BAD = "Bad";
    private const string GOOD = "Good";
    private const string LEVEL_ONE = "Level1";
    private const string FEED_ITEM = "FeedItem";
    private const string PREFABS_FOLDER = "Prefabs";
    
    private const int BETWEEN_ITEMS = 200;
    private const int START_Y_OFFSET = 100;
    private const float SCROLLING_TIME_IN_SECONDS = 100;

    private float _startTime;
    private Sprite[] _badSprites;
    private Sprite[] _goodSprites;
    private ScrollRect _scrollRect;
    private GameObject _feedItemPrefab;

    [SerializeField] private Transform _contentTransform;

    public bool EnableAutoScrolling { get; set; }

    private void Awake()
    {
        _feedItemPrefab = Resources.Load<GameObject>(PREFABS_FOLDER + Path.DirectorySeparatorChar + FEED_ITEM);
    }

    public void Setup(FeedSO feedSo)
    {
        _scrollRect = GetComponent<ScrollRect>();

        var itemLength = _feedItemPrefab.GetComponent<RectTransform>().rect.height;
        var addedOffset = new Vector3(0,  - BETWEEN_ITEMS - itemLength, 0);
        var startVectorOffset = new Vector3(0, START_Y_OFFSET, 0);

        CreateItems(feedSo.Shuffled, startVectorOffset, addedOffset);
    }

    private void CreateItems(FeedItem[] feedItemsData, Vector3 accumulatedOffset, Vector3 addedOffset)
    {
        if (feedItemsData == null) return;
        
        foreach (var feedItemData in feedItemsData)
        {
            var item = Instantiate(_feedItemPrefab, _contentTransform).GetComponent<FeedItemController>();
            item.Setup(feedItemData, accumulatedOffset);
            accumulatedOffset += addedOffset;
        }
    }

    void Update()
    {
        AutoScrolling();
    }

    private void AutoScrolling()
    {
        if (!EnableAutoScrolling) return;

        _startTime += Time.fixedDeltaTime;
        _scrollRect.verticalNormalizedPosition = Mathf.Lerp(1, 0, _startTime / SCROLLING_TIME_IN_SECONDS);
    }
}
