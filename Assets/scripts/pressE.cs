using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressE : MonoBehaviour
{
    public GameObject Instruction;        // UI prompt
    public GameObject AnimeObject;        // Door with Animator
    public AudioSource doorAudioSource;   // Sound played when door opens
    private bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instruction.SetActive(true);
            Action = true;
            Debug.Log("Player entered trigger zone");
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instruction.SetActive(false);
            Action = false;
            Debug.Log("Player exited trigger zone");
        }
    }

    void Update()
    {
        if (Action && Input.GetKeyDown(KeyCode.E))
        {
            Instruction.SetActive(false);
            AnimeObject.GetComponent<Animator>().Play("dooropenleft");

            // Play the door opening sound if available
            if (doorAudioSource != null)
            {
                doorAudioSource.Play();
                Debug.Log("Door sound played");
            }
            else
            {
                Debug.LogWarning("No AudioSource assigned to doorAudioSource");
            }

            Action = false;
        }
    }
}
