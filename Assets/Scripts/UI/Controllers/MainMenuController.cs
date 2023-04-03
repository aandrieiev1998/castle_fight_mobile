using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button settingsButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(MenuOnClick);
        exitButton.onClick.AddListener(ExitOnClick);
    }

    public void ExitOnClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE
        Application.Quit(0);

#endif
    }

    public void MenuOnClick()
    {
        SceneManager.LoadScene(1);
    }
}