using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] List<Image> _uiFoodIcons;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _powerUpSound;
    [SerializeField] AudioClip _fullInventorySound;
    [SerializeField] float itemCountVariationPitch = 0.2f;

    Player _player;
    int _lastFoodInventoryCount = 0;

    void Awake()
    {
        _player = GameManager.Instance.Player;
        _lastFoodInventoryCount = 0;
    }

    void OnEnable()
    {
        _player.playerFoodInventoryUpdated += OnPlayerFoodInventoryUpdated;
        _player.playerFoodInventoryUpdateFailed += OnPlayerFoodInventoryUpdateFailed;
    }

    void OnDisable()
    {
        _player.playerFoodInventoryUpdated -= OnPlayerFoodInventoryUpdated;
        _player.playerFoodInventoryUpdateFailed -= OnPlayerFoodInventoryUpdateFailed;
    }

    void Update()
    {

    }

    void UpdateIcons()
    {
        List<Food> playerFoods = _player.GetFoodListCopy();

        for (int i = 0; i < playerFoods.Count; i++)
        {
            _uiFoodIcons[i].enabled = true;
            _uiFoodIcons[i].sprite = playerFoods[i].ImgBig;
        }

        for (int i = playerFoods.Count; i < _uiFoodIcons.Count; i++)
        {
            _uiFoodIcons[i].enabled = false;
            _uiFoodIcons[i].sprite = null;
        }

        if (playerFoods.Count > _lastFoodInventoryCount)
        {
            //New Food Arrived
            PlayNewFoodAudio(playerFoods.Count);
            _uiFoodIcons[playerFoods.Count-1].transform.DOPunchScale(transform.localScale * 1.07f, 0.5f, 1, 0.1f);
        }
        else
        {
            //A food was delivered.
        }

        _lastFoodInventoryCount = playerFoods.Count;
    }

    void OnPlayerFoodInventoryUpdated()
    {
        UpdateIcons();
    }

    void OnPlayerFoodInventoryUpdateFailed()
    {
        PlayInventoryFullAudio();
        foreach (Image img in _uiFoodIcons)
        {
            img.transform.DOShakeRotation(0.3f,30,10,0);
        }
    }

    void PlayInventoryFullAudio()
    {
        _audioSource.pitch = 1f;
        _audioSource.PlayOneShot(_fullInventorySound);
    }

    void  PlayNewFoodAudio(int foodCount)
    {
        if (foodCount <= 0)
            return;

        _audioSource.pitch = 1f + itemCountVariationPitch * (foodCount-1);
        _audioSource.PlayOneShot(_powerUpSound);
    }
}
