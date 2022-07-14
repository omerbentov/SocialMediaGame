using Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = "Feed", menuName = "SO/Feed")]
public class FeedSO : ScriptableObject
{
    public FeedItemSO[] GoodItems;
    public FeedItemSO[] Badtems;

    public FeedItemSO[] Shuffled
    {
        get
        {
            return GoodItems.Shuffle(Badtems);
        }
    }
}
