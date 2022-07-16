using UnityEngine;

public class LevelManager 
{
    private GameObject _feed;
    private FeedView _currentLevel;
    private int _currentLevelNumber;

    public LevelManager()
    {
        _feed = GameObject.Instantiate(Client.Client.Instance.Configuration.FeedCanvasPrefab);
        LoadLevel(0);
    }

    public void LoadLevel(int levelNumber)
    {
        _currentLevelNumber = levelNumber;
        Load(levelNumber);
        StartLevel();
    }

    private void Load(int levelNumber)
    {
        if (_currentLevel != null)
        {
            foreach (Transform child in _currentLevel._contentParent.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        _currentLevel = _feed.GetComponentInChildren<FeedView>();
        var data = Client.Client.Instance.Configuration.Feeds[levelNumber];
        _currentLevel.Setup(data);
    }

    private void StartLevel()
    {
        _currentLevel.EnableAutoScrolling = Client.Client.Instance.Configuration.EnableAutoScroll;
        _currentLevel.ScrollEndedEvent += OnScrollEndedEvent;
    }

    private void OnScrollEndedEvent()
    {
        _currentLevel.ScrollEndedEvent -= OnScrollEndedEvent;
        _currentLevelNumber++;
        LoadLevel(_currentLevelNumber);
    }
}
