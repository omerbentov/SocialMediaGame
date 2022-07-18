using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Main", menuName = "SO/Main")]
public class SocialGameConfigurationSO : ConfigurationSO
{
    [HeaderAttribute("Social media")]
    public bool EnableAutoScroll;
    public GameObject FeedCanvasPrefab;
    public GameObject FeedItemPrefab;
    public FeedSO[] Feeds;

    private Dictionary<eCollectibleType, int> _collectedData = new Dictionary<eCollectibleType, int>();
    
    public int Collected(eCollectibleType type, int count = 1)
    {
        if (!_collectedData.ContainsKey(type))
        {
            _collectedData.Add(type, 0);
        }

        _collectedData[type] += count;
        
        return _collectedData[type];
    }
}