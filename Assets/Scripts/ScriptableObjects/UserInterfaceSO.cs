using UnityEngine;

[CreateAssetMenu(fileName = "UserInterfaceSO", menuName = "SO/User Interface")]
public class UserInterfaceSO : ScriptableObject
{
    public GameObject Canvas;
    
    // Collectiables
    public bool CollectiblesEnabled;
    public Sprite CollectibleSprite;
    public GameObject CollectiblePrefab;
    public GameObject CollectibleWidgetPrefab;
    public float CollectibleTimerToMove;
}
