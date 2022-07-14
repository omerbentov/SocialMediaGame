using UnityEngine;

[CreateAssetMenu(fileName = "FeedItem", menuName = "Feed Item")]
public class FeedItem : ScriptableObject
{
    public int Likes;
    public string Title;
    public Sprite Sprite;
    public bool IsGood;
}
