using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject overlay;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button unpauseButton;

        private void Start()
        {
            menuButton.onClick.AddListener(MenuButtonOnClick);
            unpauseButton.onClick.AddListener(UnpauseButtonOnClick);
        }

        private void MenuButtonOnClick()
        {
            overlay.SetActive(!overlay.activeSelf);
        }

        private void UnpauseButtonOnClick()
        {
            overlay.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                overlay.SetActive(!overlay.activeSelf);
            }
        }
    }
}