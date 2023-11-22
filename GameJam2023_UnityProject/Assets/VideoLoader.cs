using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] string _videoNameWithExtension;

    void Start()
    {
        string videoUrl = Application.streamingAssetsPath + "/" + _videoNameWithExtension;
        _videoPlayer.url = videoUrl;
        _videoPlayer.Play();
    }
}
