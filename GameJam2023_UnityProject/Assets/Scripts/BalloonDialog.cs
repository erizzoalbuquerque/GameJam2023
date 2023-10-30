using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonDialog : MonoBehaviour
{
    [SerializeField] SpriteRenderer _iconSpriteRenderer;
    [SerializeField] GameObject _balloon;
    [SerializeField] SpriteRenderer _fillerSpriteRenderer;
    [SerializeField] Color _fillerAlmostTimeOutColor;

    Material _fillerMaterial;

    float _timeOut;
    float _startTime;
    bool _isRunning;
    Color _fillerNormalColor;

    // Start is called before the first frame update
    void Start()
    {
        ShutUp();

        _fillerMaterial = _fillerSpriteRenderer.material;

        _fillerNormalColor = _fillerSpriteRenderer.material.color;
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

    public void UpdateFiller(float progress)
    {
        _fillerMaterial.SetFloat("_ClipUvRight", 1f - progress);
        if (progress > 0.7)
        {
            _fillerMaterial.color = _fillerAlmostTimeOutColor;
            _balloon.transform.localScale = Vector3.one * (1f + 0.2f * Mathf.Abs(Mathf.Sin( (progress-0.7f) * Mathf.PI * 10f)));
        }
        else
        {
            _fillerMaterial.color = _fillerNormalColor;
        }
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
