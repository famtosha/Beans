public class ServerInfo
{
    public int playerLimit;
    public int playerCount;
    public int mapID;

    public ServerInfo()
    {

    }

    public ServerInfo(int playerLimit, int playerCount, int mapID)
    {
        this.playerLimit = playerLimit;
        this.playerCount = playerCount;
        this.mapID = mapID;
    }

    public override string ToString()
    {
        return $"map:{mapID} players: {playerCount}/{playerLimit}";
    }
}
