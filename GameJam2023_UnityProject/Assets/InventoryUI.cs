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
        _player.playerFoooInventoryUpdated += OnPlayerFoodInventoryUpdated;
    }

    void OnDisable()
    {
        _player.playerFoooInventoryUpdated -= OnPlayerFoodInventoryUpdated;
    }

    void Update()
    {

    }

    void UpdateIcons()
    {
        List<Food> playerFoods = _player.GetFoodListCopy();        
        
        for(int i = 0; i < playerFoods.Count; i++)
        {
            _uiFoodIcons[i].sprite = playerFoods[i].Img;
        }
    }

    void OnPlayerFoodInventoryUpdated()
    {
        UpdateIcons();
    }
}
