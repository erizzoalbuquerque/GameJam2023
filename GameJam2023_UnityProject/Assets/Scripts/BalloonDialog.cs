using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonDialog : MonoBehaviour
{
    [SerializeField] SpriteRenderer _iconSpriteRenderer;
    [SerializeField] GameObject _balloon;
    [SerializeField] SpriteRenderer _fillerSpriteRenderer;

    Material _fillerMaterial;

    float _timeOut;
    float _startTime;
    bool _isRunning;

    // Start is called before the first frame update
    void Start()
    {
        ShutUp();

        _fillerMaterial = _fillerSpriteRenderer.material;
        print(_fillerMaterial.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning && _timeOut > 0f)
        {
            float progress = (Time.time - _startTime) / (_timeOut);
            UpdateFiller(progress);
        }                
    }

    public void Say(Sprite image, float timeOut = 0f)
    {
        _iconSpriteRenderer.sprite = image;
        _timeOut = timeOut;
        _startTime = Time.time;

        CreateBalloon();
    }

    public void ShutUp()
    {
        RemoveBalloon();
    }

    void UpdateFiller(float progress)
    {
        _fillerMaterial.SetFloat("_ClipUvRight", 1f - progress);
    }

    void CreateBalloon()
    {
        _balloon.SetActive(true);
        _balloon.transform.localScale = Vector3.zero;
        _balloon.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.OutElastic);
        UpdateFiller(0f);
        _isRunning = true;
    }

    void RemoveBalloon()
    {
        _balloon.SetActive(false);
        _isRunning = false;
    }
}
