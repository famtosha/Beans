using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private static IPEndPoint serverIP;
    private static string playerName;
    private static int mapID;

    public int menu;
    public int game;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() => SceneManager.activeSceneChanged += OnSceneChanged;
    private void OnDisable() => SceneManager.activeSceneChanged -= OnSceneChanged;

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.buildIndex == game)
        {
            FindObjectOfType<ClientBehaviour>().ConnectToServer(serverIP, playerName);
            FindObjectOfType<MapLoader>().LoadMap(mapID);
        }
    }

    public void LoadGameScene(IPEndPoint serverIP, string playerName, int mapID)
    {
        SceneLoader.serverIP = serverIP;
        SceneLoader.playerName = playerName;
        SceneLoader.mapID = mapID;
        SceneManager.LoadScene(game);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
