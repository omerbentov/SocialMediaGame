using UnityEngine;

public class UIController
{
    private readonly GameObject _canvas;
    private readonly UserInterfaceSO _model;
    private CollectibleView _collectibleView;

    public UIController()
    {
        _model = Client.Client.Instance.Configuration.UserInterfaceSo;
        _canvas = GameObject.Instantiate(_model.Canvas);
        
        Setup();
        AddListeners();
    }

    private void Setup()
    {
        if (_model.CollectiblesEnabled)
        {
            CreateCollectibles();
        }
    }

    private void CreateCollectibles()
    {
        _collectibleView = GameObject.Instantiate(_model.CollectiblePrefab, _canvas.transform).GetComponent<CollectibleView>();
        _collectibleView.Setup(0, _model.CollectibleSprite);
    }

    private void AddListeners()
    {
        Client.Client.Instance.Broadcaster.Add<CollectiblesCountChangedEvent>(OnCollectibleCountChanged);
        Client.Client.Instance.Broadcaster.Add<ItemClickedEvent>(OnItemClickedEvent);
    }

    private void OnItemClickedEvent(ItemClickedEvent eventData)
    {
        Client.Client.Instance.Data.AddCollectibles(eventData.IsGood ? 1 : -1);
    }

    private void OnCollectibleCountChanged(CollectiblesCountChangedEvent eventData)
    {
        _collectibleView.SetCollectiblesCount(eventData.Count);
    }
}