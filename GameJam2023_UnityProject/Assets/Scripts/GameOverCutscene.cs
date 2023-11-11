using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class GameOverCutscene : MonoBehaviour
{

    void OnEnable()
    {
        Time.timeScale = 0.5f;

        List<AudioSource> sources = FindObjectsOfType<AudioSource>().ToList();

        foreach (AudioSource source in sources) 
        {
            source.pitch *= Time.timeScale; 
        }
    }

    void OnDisable() 
    {
        Time.timeScale = 1f;
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
