using UnityEngine;

public class ConvexHullParent : MonoBehaviour
{
    public void Resize()
    {
        var newHeight = transform.childCount * 400 - ((transform.childCount - 1) * 100);
        var rect = GetComponent<RectTransform>().rect;
        rect.height = newHeight;
    }
}
