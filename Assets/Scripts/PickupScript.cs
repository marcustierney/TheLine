using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform playerCamera;
    public Transform holdPosition;
    public float pickupRange = 10f;
    public float moveSpeed = 10f;
    public PortalDrawer portalDrawer; 

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
            MoveHeldObject();
    }

    void TryPickup()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.blue, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.CompareTag("Portal"))
            {
                Debug.Log("Ray hit portal: " + hit.collider.name);

                //Calculate redirected ray through portal
                if (portalDrawer.currentPortal != null && portalDrawer.portalViewCamera != null)
                {
                    Debug.Log("Calculating redirected");
                    //Convert hit point from player space to portal camera space
                    Vector3 localPoint = portalDrawer.currentPortal.transform.InverseTransformPoint(hit.point);
                    Vector3 newWorldPoint = portalDrawer.portalViewCamera.transform.TransformPoint(localPoint);

                    //Cast a ray from portal camera in its forward direction
                    Ray redirectedRay = new Ray(newWorldPoint, portalDrawer.portalViewCamera.transform.forward);

                    if (Physics.Raycast(redirectedRay, out RaycastHit portalHit, 100f))
                    {
                        Debug.DrawLine(newWorldPoint, portalHit.point, Color.green, 2f);
                        AttemptPickup(portalHit);
                        return; 
                    }
                }
            }
            //else statement
            AttemptPickup(hit);
        }
    }

    void AttemptPickup(RaycastHit hit)
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

    void MoveHeldObject()
    {
        Vector3 targetPos = holdPosition.position;
        Vector3 moveDir = (targetPos - heldObject.position);
        heldObject.linearVelocity = moveDir * moveSpeed;
        heldObject.MoveRotation(Quaternion.Lerp(heldObject.rotation, holdPosition.rotation, Time.deltaTime * 10f));
    }

    void DropObject()
    {
        heldObject.useGravity = true;
        heldObject.linearDamping = 0f;
        heldObject.angularDamping = 0.05f;
        heldObject = null;
    }
}

/*using UnityEngine;

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
}*/