using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyOpenDoor : MonoBehaviour
{
    public GameObject Instruction;          // UI prompt to press 'E'
    public GameObject AnimeObject;          // Door object with Animator
    public AudioSource doorAudioSource;     // Sound played when door opens
    public AudioSource officeAudioSource;   // Additional office ambient sound

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
            AnimeObject.GetComponent<Animator>().Play("New Animation");

            if (doorAudioSource != null)
            {
                doorAudioSource.Play();
                Debug.Log("Door sound played");
            }

            if (officeAudioSource != null)
            {
                officeAudioSource.Play();
                Debug.Log("Office ambient sound played");
            }

            Action = false;
            Debug.Log("Door opening animation triggered");
        }
    }
}
