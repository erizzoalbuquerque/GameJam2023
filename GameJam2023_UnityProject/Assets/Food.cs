using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Food")]
public class Food : ScriptableObject
{
    [SerializeField] string foodName;
    [SerializeField] int price;
    [SerializeField] private int avgConsumeTime;
    [SerializeField] private Sprite img;

    public string FoodName { get => foodName; }
    public int Price { get => price; }
    public int AvgConsumeTime { get => avgConsumeTime; }
    public Sprite Img { get => img; }
}
