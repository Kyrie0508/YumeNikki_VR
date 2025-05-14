using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class classSceneLoader : MonoBehaviour
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
            fader.FadeToScene("class_nikki");
        }
    }
}
