using UnityEngine;

[CreateAssetMenu(fileName = "Main", menuName = "SO/Main")]
public class SocialGameConfigurationSO : ConfigurationSO
{
    [HeaderAttribute("Social media")]
    public bool EnableAutoScroll;
    public GameObject FeedCanvasPrefab;
    public GameObject FeedItemPrefab;
    public FeedSO[] Feeds;
}