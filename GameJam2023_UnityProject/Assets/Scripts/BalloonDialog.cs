using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonDialog : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] GameObject _balloon;

    // Start is called before the first frame update
    void Start()
    {
        ShutUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Say(Sprite image)
    {
        _spriteRenderer.sprite = image;
        CreateBallon();
    }

    public void ShutUp()
    {
        RemoveBaloon();
    }

    void CreateBallon()
    {
        _balloon.SetActive(true);
        _balloon.transform.localScale = Vector3.zero;
        _balloon.transform.DOScale(Vector3.one,0.5f);
    }

    void RemoveBaloon()
    {
        _balloon.SetActive(false);
    }
}
