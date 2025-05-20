using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour
{
    public Animator npcAnimator;
    public GameObject instructionUI;
    private bool playerNear = false;
    public AudioSource npcAudioSource;

    [Header("Path Visualization")]
    public PathVisualizer pathVisualizer;
    public Transform receptionTarget;
    public Transform interviewTarget;
    public Transform interviewZone;


    private bool hasInteracted = false;

    void Start()
    {
        if (instructionUI != null)
            instructionUI.SetActive(false);

        // Show path to reception at start
        if (pathVisualizer != null && receptionTarget != null)
        {
            pathVisualizer.destination = receptionTarget;
            pathVisualizer.ShowPath();
        }
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            hasInteracted = true;

            if (instructionUI != null)
                instructionUI.SetActive(false);

            npcAnimator.SetTrigger("StartConversation");
            Debug.Log("NPC starts conversation");

            if (npcAudioSource != null)
                npcAudioSource.PlayDelayed(1.5f); // Delay in seconds


            // Show path to interview
            if (pathVisualizer != null && interviewTarget != null)
            {
                pathVisualizer.ClearPath();
                pathVisualizer.destination = interviewTarget;
                pathVisualizer.ShowPath();
            }

            // Simulate end of conversation after X seconds (e.g. 5s)
            StartCoroutine(ResetInteractionAfterDelay(5f));
        }
    }

    IEnumerator ResetInteractionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasInteracted = false;
        Debug.Log("Interaction reset. Player can talk to NPC again.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");
            playerNear = true;

            if (instructionUI != null)
                instructionUI.SetActive(true);

            // Stop showing the path if player enters the interview zone
            if (interviewZone != null &&
                Vector3.Distance(other.transform.position, interviewZone.position) < 2f)
            {
                Debug.Log("Player arrived at interview room. Stopping path.");
                if (pathVisualizer != null)
                    pathVisualizer.ClearPath();
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left trigger zone.");
            playerNear = false;
            if (instructionUI != null)
                instructionUI.SetActive(false);
        }
    }
}
