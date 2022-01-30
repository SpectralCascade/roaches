using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScipt : MonoBehaviour
{

    bool paused = true;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnPause()
    {
        paused = !paused;

        if(paused)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
