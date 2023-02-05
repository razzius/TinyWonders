using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startCamera;
    public GameObject startUi;
    public GameObject gameUi;
    
    private static GameManager instance;

    private bool currentlyPlaying = false;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!currentlyPlaying)
        {
            if (Input.GetButtonDown("Submit"))
            {
                startCamera.SetActive(false);
                startUi.SetActive(false);
                gameUi.SetActive(true);
                currentlyPlaying = true;
            }
            
            if (Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                startCamera.SetActive(true);
                startUi.SetActive(true);
                gameUi.SetActive(false);
                currentlyPlaying = false;
            }
        }
    }
}
