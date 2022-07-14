using Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = "Feed", menuName = "SO/Feed")]
public class FeedSO : ScriptableObject
{
    public FeedItem[] GoodItems;
    public FeedItem[] Badtems;

    public FeedItem[] Shuffled
    {
        get
        {
            return GoodItems.Shuffle(Badtems);
        }
    }
}
