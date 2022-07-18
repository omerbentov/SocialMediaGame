using UnityEngine;

public class ConfigurationSO : ScriptableObject
{
    [HeaderAttribute("UI")]
    public GameObject Canvas;
    
    [HeaderAttribute("Collectibles")]
    public GameObject CollectiblePrefab;
    public GameObject CollectibleWidgetPrefab;
    public float CollectibleTimerToMove;
}