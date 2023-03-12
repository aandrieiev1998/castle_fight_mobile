using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject overlay;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button unpauseButton;
        [SerializeField] private Button mainMenuButton;


        private void Start()
        {
            overlay.SetActive(false);
            menuButton.onClick.AddListener(MenuButtonOnClick);
            unpauseButton.onClick.AddListener(UnpauseButtonOnClick);
            mainMenuButton.onClick.AddListener(MainMenuButtonOnClick);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                overlay.SetActive(!overlay.activeSelf);
            }
        }

        private void MenuButtonOnClick()
        {
            overlay.SetActive(!overlay.activeSelf);
        }

        private void UnpauseButtonOnClick()
        {
            overlay.SetActive(false);
        }

        private void MainMenuButtonOnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}