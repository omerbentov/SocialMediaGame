using System;
using Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = "Feed", menuName = "SO/Feed")]
public class FeedSO : ScriptableObject
{
    public float ScrollTimeInSecounds;
    public CollectibleData[] Collectibles;
    public FeedItemSO[] GoodItems;
    public FeedItemSO[] Badtems;

    public FeedItemSO[] Shuffled
    {
        get
        {
            return GoodItems.Shuffle(Badtems);
        }
    }

    public int ItemCount
    {
        get { return GoodItems.Length + Badtems.Length; }
    }      
}

[Serializable]
public class CollectibleData
{
    public eCollectibleType Type;
    public Sprite Sprite;
    public int Count;
}
