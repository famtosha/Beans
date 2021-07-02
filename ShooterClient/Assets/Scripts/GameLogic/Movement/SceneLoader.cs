using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private static IPEndPoint serverIP;
    private static string playerName;

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
        }

        if (newScene.buildIndex == menu)
        {

        }
    }

    public void LoadGameScene(IPEndPoint serverIP, string playerName)
    {
        SceneLoader.serverIP = serverIP;
        SceneLoader.playerName = playerName;
        SceneManager.LoadScene(game);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
