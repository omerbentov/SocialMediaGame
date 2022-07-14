using UnityEngine;

[CreateAssetMenu(fileName = "FeedItem", menuName = "SO/Feed Item")]
public class FeedItemSO : ScriptableObject
{
    public int Likes;
    public string Title;
    public Sprite Sprite;
    public bool IsGood;
}
