using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameOverScript.finishTime = Time.timeSinceLevelLoad;
            GameOverScript.playerWon = true;
            SceneManager.LoadScene("WinScene");
        }
    }
}
