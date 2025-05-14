using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorSceneLoader : MonoBehaviour
{
    private SceneFader fader;
    
    void Start()
    {
        fader = FindObjectOfType<SceneFader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fader.FadeToScene("DoorNikki");
        }
    }
}
