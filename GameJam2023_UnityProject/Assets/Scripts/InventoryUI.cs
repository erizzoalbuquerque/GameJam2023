using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] List<Image> _uiFoodIcons;

    Player _player;

    void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    void OnEnable()
    {
        _player.playerFoodInventoryUpdated += OnPlayerFoodInventoryUpdated;
    }

    void OnDisable()
    {
        _player.playerFoodInventoryUpdated -= OnPlayerFoodInventoryUpdated;
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
            _uiFoodIcons[i].sprite = playerFoods[i].Img;
        }

        for (int i = playerFoods.Count; i < _uiFoodIcons.Count; i++)
        {
            _uiFoodIcons[i].enabled = false;
            _uiFoodIcons[i].sprite = null;
        }
    }

    void OnPlayerFoodInventoryUpdated()
    {
        UpdateIcons();
    }
}
