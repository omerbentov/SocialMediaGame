using System;
using UnityEngine;

public class UIController
{
    private readonly GameObject _canvas;
    private readonly UserInterfaceSO _model;
    private CollectibleWidgetView _collectibleWidgetView;

    public Transform CollectibleTransform
    {
        get { return _collectibleWidgetView.transform; }
    }

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
        _collectibleWidgetView = GameObject.Instantiate(_model.CollectibleWidgetPrefab, _canvas.transform).GetComponent<CollectibleWidgetView>();
        _collectibleWidgetView.Setup(0, _model.CollectibleSprite);
    }

    private void AddListeners()
    {
        Client.Client.Instance.Broadcaster.Add<CollectiblesCountChangedEvent>(OnCollectibleCountChanged);
        Client.Client.Instance.Broadcaster.Add<ItemClickedEvent>(OnItemClickedEvent);
    }

    private void OnItemClickedEvent(ItemClickedEvent eventData)
    {
        if (eventData.IsGood)
        {
            Collect(eventData.Position, () => { Client.Client.Instance.Data.AddCollectibles(1); });
        }
        else
        {
            Client.Client.Instance.Data.AddCollectibles(-1);
        }
    }

    private void Collect(Vector3 position, Action OnComplete)
    {
        var collectible = 
            GameObject.Instantiate(
                Client.Client.Instance.Configuration.UserInterfaceSo.CollectiblePrefab,
                Client.Client.Instance.UI.CollectibleTransform)
            .GetComponent<MoveToPostion>();

        collectible.Setup(position, OnComplete);
    }

    private void OnCollectibleCountChanged(CollectiblesCountChangedEvent eventData)
    {
        _collectibleWidgetView.SetCollectiblesCount(eventData.Count);
    }
}