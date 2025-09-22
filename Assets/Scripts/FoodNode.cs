using System;
using UnityEngine;

[Serializable]
public class FoodNode
{
    [SerializeField] private Food.foodType foodType;
    [SerializeField] private Food.cookLevel cookLevel;

    public Food.foodType FoodType => foodType;
    public Food.cookLevel CookLevel => cookLevel;


    public FoodNode(Food.foodType type, Food.cookLevel heat)
    {
        foodType = type;
        cookLevel = heat;
    }
}
