using Client;

public class DataManager
{
    private IClient _client;
    private int _collectiblesCount { get; set; }

    public DataManager()
    {
        _client = Client.Client.Instance;
    }

    public void AddCollectibles(int countToAdd)
    {
        _collectiblesCount += countToAdd;
        Client.Client.Instance.Broadcaster.Broadcast(
            new CollectiblesCountChangedEvent() { Count = _collectiblesCount });
    }
}

public class CollectiblesCountChangedEvent
{
    public int Count;
}