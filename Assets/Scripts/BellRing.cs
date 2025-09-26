using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BellRing : MonoBehaviour , ITouchable
{
    private List<Food> foodList = new List<Food>();

    public void Interact(GameObject player)
    {
        if (foodList.Count <= 0)
        {
            TextManager.Instance.StartText();
        }
        else
        {    
            var mistakes = FoodCompareManager.manager.CompareFood(foodList);
            var points = 50f;

            points -= mistakes.Count * 10;

            player.GetComponent<MichaelMovement>().AddPoints(points);
        }
    }

    public void AddToList(Food food)
    {
        foodList.Add(food);
    }

    public void RemoveFromList(Food food)
    {
        foodList.Remove(food);
    }
}
