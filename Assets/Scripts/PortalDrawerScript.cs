using UnityEngine;
using System.Collections.Generic;

public class PortalDrawer : MonoBehaviour
{
    public Transform playerCamera;
    public GameObject pointPrefab;
    public GameObject portalQuadPrefab;
    public Camera portalViewCamera; 

    private List<Vector3> placedPoints = new List<Vector3>();
    private List<GameObject> currentMarkers = new List<GameObject>();
    public GameObject currentPortal;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlacePoint();
        }
    }

    void PlacePoint()
    {
        Vector3 pointPos = playerCamera.position + playerCamera.forward * 5f;

        GameObject marker = Instantiate(pointPrefab, pointPos, Quaternion.identity);
        currentMarkers.Add(marker);
        placedPoints.Add(pointPos);

        if (placedPoints.Count == 4)
        {
            CreatePortalWindow();
        }
    }

    void CreatePortalWindow()
    {
        //Get rid of old portal
        if (currentPortal != null)
            Destroy(currentPortal);

        //Create the new portal and position 
        GameObject portal = Instantiate(portalQuadPrefab);
        portal.transform.position = placedPoints[0];
        currentPortal = portal;
        Mesh m = new Mesh();
        portal.GetComponent<MeshFilter>().mesh = m;

        //Convert world points into local space relative to portal transform
        Vector3[] localVerts = new Vector3[4];
        for (int i = 0; i < 4; i++)
            localVerts[i] = portal.transform.InverseTransformPoint(placedPoints[i]);

        m.vertices = localVerts;

        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 1);
        m.uv = uvs;

        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        m.RecalculateNormals();
        m.RecalculateBounds();
        placedPoints.Clear();

        //Assign the mesh to the collider for player raycasts
        MeshCollider meshCollider = portal.GetComponent<MeshCollider>();
        if (meshCollider == null)
            meshCollider = portal.AddComponent<MeshCollider>();

        meshCollider.sharedMesh = m;
        meshCollider.convex = false;
        meshCollider.isTrigger = false;
    }

    public bool TryRaycastThroughPortal(Ray ray, RaycastHit hit, out RaycastHit redirectedHit)
    {
        redirectedHit = new RaycastHit();
        if (currentPortal == null || portalViewCamera == null)
            return false;

        if (hit.collider.gameObject != currentPortal && hit.collider.transform.parent != currentPortal)
            return false;

        Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
        //Convert hit point from player space to portal camera space
        Vector3 localPoint = currentPortal.transform.InverseTransformPoint(hit.point);
        Vector3 newWorldPoint = portalViewCamera.transform.TransformPoint(localPoint);

        //Fire a ray from the portal camera in its forward direction
        Ray redirectedRay = new Ray(portalViewCamera.transform.position, portalViewCamera.transform.forward);

        if (Physics.Raycast(redirectedRay, out RaycastHit newHit, 100f))
        {
            redirectedHit = newHit;
            return true;
        }

        return false;
    }
}
