using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOverScene");
            Destroy(gameObject);
        }
    }
}
