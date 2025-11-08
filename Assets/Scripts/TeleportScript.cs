using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    public Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController a = other.GetComponent<CharacterController>();
            a.enabled = false; //Disable controller
            other.transform.position = teleportTarget.position;
            a.enabled = true; //Renable controller
        }
    }
}
