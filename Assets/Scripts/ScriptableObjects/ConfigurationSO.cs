using UnityEngine;

public class ConfigurationSO : ScriptableObject
{
    [HeaderAttribute("UI")]
    public GameObject Canvas;
    
    [HeaderAttribute("Collectibles")]
    public bool CollectiblesEnabled;
    public Sprite CollectibleSprite;
    public GameObject CollectiblePrefab;
    public GameObject CollectibleWidgetPrefab;
    public float CollectibleTimerToMove;
}