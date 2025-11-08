using UnityEngine;
using TMPro;

public class RaycastInteraction : MonoBehaviour
{
    public float interactionDistance = 10f;
    public bool doorUnlocked = false;
    public TextMeshPro LockedDoorText;
    public TextMeshPro Locked2DoorText;
    public TextMeshPro DoorBreakText;
    public TextMeshPro GrabKeyText;
    public TextMeshPro TeleportText;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    Debug.Log("door destroy");
                    Destroy(hit.collider.gameObject);
                    DoorBreakText.gameObject.SetActive(false);
                    TeleportText.gameObject.SetActive(false);
                }
                else if (hit.collider.CompareTag("ExitDoor") && doorUnlocked == true)
                {
                    LockedDoorText.gameObject.SetActive(false);
                    Locked2DoorText.gameObject.SetActive(false);
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("DoorKey"))
                {
                    doorUnlocked = true;
                    Debug.Log("door is unlocked " + doorUnlocked);
                    Destroy(hit.collider.gameObject);
                    GrabKeyText.gameObject.SetActive(false);
                }
            }
        }
    }
}