using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] SceneAsset _loadScene; 

    public void LoadScene()
    {
        SceneManager.LoadScene(_loadScene.name);
    }
}
