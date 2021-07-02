using UnityEngine;
using UnityEngine.SceneManagement;

public class CursoreLock : MonoBehaviour
{
    private bool _isHiddened = false;
    public bool isHiddened
    {
        get => _isHiddened;
        set
        {
            //add vsync here :)
            _isHiddened = value;
            if (!_isHiddened)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Screen.fullScreen = false;
                Screen.SetResolution(800, 600, FullScreenMode.Windowed);
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 240;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Screen.fullScreen = true;
                var res = Screen.currentResolution;
                Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
                QualitySettings.vSyncCount = 1;
            }
        }
    }

    private void OnEnable() => SceneManager.activeSceneChanged += OnSceneChanged;

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
        isHiddened = false;
    }

    private void Start()
    {
        isHiddened = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isHiddened = !isHiddened;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) Application.targetFrameRate = 20;
        if (Input.GetKeyDown(KeyCode.Alpha2)) Application.targetFrameRate = 40;
        if (Input.GetKeyDown(KeyCode.Alpha3)) Application.targetFrameRate = 60;
        if (Input.GetKeyDown(KeyCode.Alpha4)) Application.targetFrameRate = 120;
        if (Input.GetKeyDown(KeyCode.Alpha5)) Application.targetFrameRate = 240;
    }
}
