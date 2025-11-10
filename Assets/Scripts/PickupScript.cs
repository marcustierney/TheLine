using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform playerCamera;    
    public Transform holdPosition;    
    public float pickupRange = 10f;   
    public float moveSpeed = 10f;    

    private Rigidbody heldObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }

        if (heldObject)
        {
            MoveHeldObject();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.linearDamping = 10f; 
                heldObject.angularDamping = 10f;
            }
        }
    }

    void MoveHeldObject()
    {
        Vector3 targetPos = holdPosition.position;
        Vector3 moveDir = (targetPos - heldObject.position);
        heldObject.linearVelocity = moveDir * moveSpeed; 
        heldObject.MoveRotation(Quaternion.Lerp(heldObject.rotation, holdPosition.rotation, Time.deltaTime * 10f)); //Fix rotation of cube while carrying
    }

    void DropObject()
    {
        heldObject.useGravity = true;
        heldObject.linearDamping = 0f;
        heldObject.angularDamping = 0.05f;
        heldObject = null;
    }
}