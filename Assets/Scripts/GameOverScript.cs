using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverScript : MonoBehaviour
{
    public GameObject overPanel;
    public static bool playerWon = false;
    public Button BackToMenu;
    public Button Retry;
    public TMP_Text playerTime;
    public static float finishTime = 0f;
    void Start()
    {
        playerTime.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (playerWon == true)
        {
            playerTime.gameObject.SetActive(true);
            playerTime.text = $"Finish Time: {finishTime:F2}s";
        }
        overPanel.SetActive(true);
        BackToMenu.onClick.AddListener(OnBackToMenuPressed);
        Retry.onClick.AddListener(OnRetryPressed);
    }

    public void OnBackToMenuPressed()
    {
        overPanel.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }
    public void OnRetryPressed()
    {
        playerWon = false;
        finishTime = 0f;
        overPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
}
