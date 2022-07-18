using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CollectibleClickHandler : MonoBehaviour
{
    [SerializeField] private eCollectibleType Type;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Client.Client.Instance.Broadcaster.Broadcast(new CollectibleClickedEvent()
        {
            IsGood = true, 
            Position = transform.position,
            Type = Type
        });
    }
}

public enum eCollectibleType
{
    Invalid = -1,
    Like = 0,
    Comments = 1,
    Saved = 2,
    ForwardMessage = 3
}
