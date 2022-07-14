using System;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject _feedPrefab;
    private FeedController _currentLevel;

    private void Awake()
    {
        _feedPrefab = Resources.Load<GameObject>("Prefabs" + Path.DirectorySeparatorChar + "Feed");
    }

    private void Start()
    {
        LoadLevel(1);
    }

    public void LoadLevel(int levelNumber)
    {
        Load(levelNumber);
        StartLevel();
    }

    private void Load(int levelNumber)
    {
        if (transform.childCount > 0)
        {
            Destroy(_currentLevel.gameObject);
        }
        
        _currentLevel = Instantiate(_feedPrefab, transform).GetComponent<FeedController>();
        var data = Resources.Load<FeedSO>("Feeds" + Path.DirectorySeparatorChar + "Feed" + levelNumber);
        _currentLevel.Setup(data);
    }

    private void StartLevel()
    {
        _currentLevel.EnableAutoScrolling = true;
    }
}
