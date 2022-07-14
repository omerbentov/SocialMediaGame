using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        #region --- Public Methods ---

        public static Vector2 ToVector2XY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector2 ToVector2XZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }

        public static IEnumerable<T> MergeShuffle<T>(this IEnumerable<T> lista, IEnumerable<T> listb)
        {
            var first = lista.GetEnumerator();
            var second = listb.GetEnumerator();
            
            var exhaustedA = false;
            var exhaustedB = false;
            while (!(exhaustedA && exhaustedB))
            {
                var found = false;
                
                if (!exhaustedB && (exhaustedA || Random.Range(0, 2) == 0))
                {
                    exhaustedB = !(found = second.MoveNext());
                    if (found)
                        yield return second.Current;
                }
                
                if (!found && !exhaustedA)
                {
                    exhaustedA = !(found = first.MoveNext());
                    if (found)
                        yield return first.Current;
                }
            }                
        }
        
        
        public static FeedItem[] Shuffle(this FeedItem[] goodItems, FeedItem[] badItems)
        {
            var goodPassedCount = 0;
            var badPassedCount = 0;
            var totalItems = goodItems.Length + badItems.Length;

            var feedItemsData = new FeedItem[totalItems];

            for (var i = 0; i < totalItems; i++)
            {
                var isGood = Random.value > 0.5f;
            
                if (isGood && goodPassedCount < goodItems.Length || badPassedCount >= badItems.Length)
                {
                    feedItemsData[i] = goodItems[goodPassedCount];
                    goodPassedCount++;
                }
                else
                {
                    feedItemsData[i] = badItems[badPassedCount];
                    badPassedCount++;
                }
            }

            return feedItemsData;
        }

        #endregion
    }
}