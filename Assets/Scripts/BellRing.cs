using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BellRing : MonoBehaviour , ITouchable
{
    [SerializeField] private bool objectNeeded;
    public bool ObjectNeeded { get { return objectNeeded; } }
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
            Debug.Log(mistakes.Count);
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
