using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject cubePrefab; 
    public Transform spawnPoint;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 position = spawnPoint.position;
            Instantiate(cubePrefab, position, Quaternion.identity);
        }
    }
}