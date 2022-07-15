using UnityEngine.UI;

public class CollectibleImage : Image
{
    void Start()
    {
        base.sprite = Client.Client.Instance.Configuration.UserInterfaceSo.CollectibleSprite;
    }
}
