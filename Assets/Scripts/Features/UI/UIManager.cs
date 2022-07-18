using System;
using System.Collections.Generic;
using System.Linq;
using Client;
using UnityEngine;

public class UIManager
{
    private readonly IClient _client;
    private readonly GameObject _canvas;
    private readonly SocialGameConfigurationSO _model;
    private Dictionary<eCollectibleType, CollectibleWidgetView> _collectibleWidgetViewDictionary;

    public UIManager()
    {
        _model = Client.Client.Instance.Configuration;
        _canvas = GameObject.Instantiate(_model.Canvas);
        _collectibleWidgetViewDictionary = new Dictionary<eCollectibleType, CollectibleWidgetView>();
        _client = Client.Client.Instance;

        AddListeners();
        Setup(_client.Level.CurrentData);
    }

    private void AddListeners()
    {
        _client.Broadcaster.Add<CollectibleClickedEvent>(OnItemClickedEvent);  
    }

    private void Setup(FeedSO feedSo)
    {
        CreateCollectibles(feedSo);
    }

    private void CreateCollectibles(FeedSO feedSo)
    {
        foreach (var feedSoCollectible in feedSo.Collectibles)
        {
            _collectibleWidgetViewDictionary.Add(
                feedSoCollectible.Type,
                GameObject.Instantiate(_model.CollectibleWidgetPrefab, _canvas.transform.GetChild(0))
                    .GetComponent<CollectibleWidgetView>());
            _collectibleWidgetViewDictionary[feedSoCollectible.Type].Setup(0, feedSoCollectible.Sprite);
        }
    }

    private void OnItemClickedEvent(CollectibleClickedEvent eventData)
    {
        var isBeingCollected = _collectibleWidgetViewDictionary.Keys.Contains(eventData.Type);
        
        if (eventData.IsGood && isBeingCollected)
        {
            Collect(
                eventData.Position,
                eventData.Type, 
                () => OnCollectibleCountChanged(eventData.Type, _model.Collected(eventData.Type)));
        }
    }

    private void Collect(Vector3 position, eCollectibleType eCollectibleType, Action OnComplete)
    {
        var gotoLocation = GetWidgetLocation(eCollectibleType);
        
        var collectible = 
            GameObject.Instantiate(
                _client.Configuration.CollectiblePrefab,
                gotoLocation)
            .GetComponent<MoveToPostion>();

        collectible.Setup(position, gotoLocation.position, OnComplete);
    }

    private Transform GetWidgetLocation(eCollectibleType eCollectibleType)
    {
        return _collectibleWidgetViewDictionary[eCollectibleType].transform;
    }

    private void OnCollectibleCountChanged(eCollectibleType type, int count)
    {
        _collectibleWidgetViewDictionary[type].SetCollectiblesCount(count);
    }
}