using UnityEngine;
using System.Collections.Generic;

public class PortalDrawer : MonoBehaviour
{
    public Transform playerCamera;
    public GameObject pointPrefab;
    public GameObject portalQuadPrefab;

    private List<Vector3> placedPoints = new List<Vector3>();

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlacePoint();
        }
    }

    void PlacePoint()
    {
        //Pos of each pointer (10 units)
        Vector3 pointPos = playerCamera.position + playerCamera.forward * 10f;

        //Add marker
        Instantiate(pointPrefab, pointPos, Quaternion.identity);

        //Save the point
        placedPoints.Add(pointPos);

        if (placedPoints.Count == 4)
        {
            CreatePortalWindow();
        }
    }

    //Once four points are placed, the script spawns a quad object to serve as the portal surface, positions it at the first point,
    //and converts the other three point positions into the quad’s local space so the mesh lines up exactly with the placed markers.
    //The script then constructs a mesh using those four local points and applies triangle indices to create a visible surface,
    //forming a portal-shaped plane in the exact location drawn by the player.
    void CreatePortalWindow()
    {
        //Create the portal and position it at the first point
        GameObject portal = Instantiate(portalQuadPrefab);
        portal.transform.position = placedPoints[0];

        Mesh m = new Mesh();
        portal.GetComponent<MeshFilter>().mesh = m;

        //Convert world points into local space relative to portal transform
        Vector3[] localVerts = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            localVerts[i] = portal.transform.InverseTransformPoint(placedPoints[i]);
        }

        m.vertices = localVerts;

        // Assuming your four points form a rectangle in order:
        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 1);
        m.uv = uvs;

        //Triangles
        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        m.RecalculateNormals();
        m.RecalculateBounds();

        placedPoints.Clear();
    }
}