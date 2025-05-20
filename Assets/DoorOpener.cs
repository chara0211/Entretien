using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject[] doors;  // All doors to rotate
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool isOpen = false;
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            Debug.Log("Toggling door state: " + (isOpen ? "Open" : "Closed"));
        }

        foreach (GameObject door in doors)
        {
            Transform doorPivot = door.transform;

            Quaternion targetRotation = Quaternion.Euler(0, isOpen ? openAngle : 0, 0);
            doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, targetRotation, Time.deltaTime * openSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player entered door trigger zone.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Player exited door trigger zone.");
        }
    }
}
