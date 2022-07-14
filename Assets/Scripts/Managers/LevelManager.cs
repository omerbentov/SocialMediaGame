using System;
using System.IO;
using UnityEngine;

public class LevelManager 
{
    private GameObject _feed;
    private FeedView _currentLevel;

    public LevelManager()
    {
        _feed = GameObject.Instantiate(
            Resources.Load<GameObject>("Prefabs" + Path.DirectorySeparatorChar + "FeedCanvas"));
        LoadLevel(1);
    }

    public void LoadLevel(int levelNumber)
    {
        Load(levelNumber);
        StartLevel();
    }

    private void Load(int levelNumber)
    {
        if (_currentLevel != null)
        {
            GameObject.Destroy(_currentLevel.gameObject);
        }
        
        _currentLevel = _feed.GetComponentInChildren<FeedView>();
        var data = Resources.Load<FeedSO>("Configuration" + Path.DirectorySeparatorChar + "Feeds" + Path.DirectorySeparatorChar + "Feed" + levelNumber);
        _currentLevel.Setup(data);
    }

    private void StartLevel()
    {
        _currentLevel.EnableAutoScrolling = true;
    }
}
