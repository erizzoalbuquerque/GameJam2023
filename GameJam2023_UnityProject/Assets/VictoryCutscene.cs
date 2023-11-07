using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCutscene : MonoBehaviour
{
    [SerializeField] float cameraAnimationTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        float targetSize = 8;
        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, targetSize, cameraAnimationTime).SetEase(Ease.InCubic);
    }
}
