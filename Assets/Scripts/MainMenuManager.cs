using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    //public GameObject loadingPanel;
    public Button StartButton;
    public Button OptionsButton;
    public Button CreditsButton;
    public Button EasyButton;
    public Button HardButton;
    public Button BackOptionsButton;
    public Button BackCreditButton;
    public static float levelTime = 120;
    public void StartGame() => SceneManager.LoadScene("GameScene");

    private void Start()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        StartButton.onClick.AddListener(OnStartPressed);
        OptionsButton.onClick.AddListener(OnOptionsPressed);
        CreditsButton.onClick.AddListener(OnCreditsPressed);
        BackOptionsButton.onClick.AddListener(OnBackPressed);
        BackCreditButton.onClick.AddListener(OnBackCreditsPressed);
        EasyButton.onClick.AddListener(OnEasyPressed);
        HardButton.onClick.AddListener(OnHardPressed);
    }
    public void OnStartPressed()
    {
        mainPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
    public void OnOptionsPressed()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void OnCreditsPressed()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void OnBackPressed()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    public void OnBackCreditsPressed()
    {
        Debug.Log("BackCredits pressed!");
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void OnEasyPressed()
    {
        MainMenuManager.levelTime = 120f;
    }
    public void OnHardPressed()
    {
        MainMenuManager.levelTime = 45f;
    }
}
