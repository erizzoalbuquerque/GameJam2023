using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _maxNumberOfFoods = 3;

    List<Food> _foods = new List<Food>();

    public event Action playerFoodInventoryUpdated;
    public event Action playerFoodInventoryUpdateFailed;

    public int MaxNumberOfFoods { get => _maxNumberOfFoods;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddFood(Food food)
    {
        if (_foods.Count < _maxNumberOfFoods)
        {
            _foods.Add(food);
            playerFoodInventoryUpdated.Invoke();
        }  
        else
        {
            playerFoodInventoryUpdateFailed();
        }
    }

    public void DeliverFood(Food food)
    {
        if (_foods.Contains(food))
        {
            _foods.Remove(food);
            playerFoodInventoryUpdated.Invoke();
        }
    }

    public bool HasFood(Food food)
    {
        return _foods.Contains(food);
    }

    public List<Food> GetFoodListCopy()
    {
        return new List<Food>(_foods);
    }

    public void ThrowAway()
    {
        _foods.Clear();
        playerFoodInventoryUpdated.Invoke();
    }
}
