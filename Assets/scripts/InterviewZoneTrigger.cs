using UnityEngine;

public class InterviewZoneTrigger : MonoBehaviour
{
    public PathVisualizer pathVisualizer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && pathVisualizer != null)
        {
            Debug.Log("Player entered the interview zone.");

            // Disable and clear the path so it doesn't redraw in Update()
            pathVisualizer.DisablePath();
        }
    }
}
