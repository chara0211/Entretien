using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathVisualizer : MonoBehaviour
{
    public Transform player;               // Drag the player object here
    public Transform destination;          // This can be set dynamically
    private LineRenderer lineRenderer;
    private NavMeshPath navMeshPath;
    public bool showPath = true;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        navMeshPath = new NavMeshPath();
    }

    void Update()
    {
        if (showPath && player != null && destination != null)
        {
            DrawPath();
        }
        else
        {
            lineRenderer.positionCount = 0; // Clear only rendering, not logic flag
        }
    }

    private void DrawPath()
    {
        if (NavMesh.CalculatePath(player.position, destination.position, NavMesh.AllAreas, navMeshPath))
        {
            if (navMeshPath.corners.Length < 2)
                return;

            lineRenderer.positionCount = navMeshPath.corners.Length;

            for (int i = 0; i < navMeshPath.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, navMeshPath.corners[i] + Vector3.up * 0.1f);
            }
        }
    }

    public void ClearPath()
    {
        lineRenderer.positionCount = 0;
        // Do NOT disable showPath here unless you really want to stop showing paths entirely
    }

    public void EnablePath()
    {
        showPath = true;
    }

    public void DisablePath()
    {
        showPath = false;
        ClearPath(); // Optionally also clear visual
    }
    public void ShowPath()
    {
        if (NavMesh.CalculatePath(player.position, destination.position, NavMesh.AllAreas, navMeshPath))
        {
            if (navMeshPath.corners.Length < 2)
            {
                ClearPath();
                return;
            }

            lineRenderer.positionCount = navMeshPath.corners.Length;

            for (int i = 0; i < navMeshPath.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, navMeshPath.corners[i] + Vector3.up * 0.1f);
            }
        }
    }

}
