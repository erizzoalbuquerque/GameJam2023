using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class CustomerPayText : MonoBehaviour
{
    [SerializeField] Color _positiveColor = Color.green;
    [SerializeField] Color _negativeColor = Color.red;
    [SerializeField] float _duration = 1f;
    [SerializeField] float _animationDistance = 0.5f;

    TextMeshPro _textMeshPro;
    Vector3 _startLocalPosition;
    float _timer;

    void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _startLocalPosition = this.transform.localPosition;
        _textMeshPro.enabled = false;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;

            if(_timer <= 0 )
            {
                _textMeshPro.enabled = false;
            }
        }
    }

    public void Play(int amountPaid)
    {
        _textMeshPro.enabled = true;

        StringBuilder sb = new StringBuilder();
        if (amountPaid >= 0)
        {
            _textMeshPro.color = _positiveColor;
            sb.Append("+");
        }
        else
        {
            _textMeshPro.color= _negativeColor;
            //sb.Append("-");
        }

        sb.Append(amountPaid);

        print(sb.ToString());
        _textMeshPro.text = sb.ToString();

        this.transform.localPosition = _startLocalPosition;
        this.transform.DOLocalMove(_startLocalPosition + Vector3.up * _animationDistance, _duration).SetEase(Ease.OutCubic);

        _timer = _duration;
    }
}
