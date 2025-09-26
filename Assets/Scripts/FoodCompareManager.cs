using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class FoodCompareManager : MonoBehaviour
{
    static public FoodCompareManager manager;

    [SerializeField] private List<FoodNode> clientOrder = new List<FoodNode>();
    private List<FoodNode> mistakes = new List<FoodNode>();

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetClientOrder(List<FoodNode> order)
    {
        clientOrder.Clear();
        clientOrder.AddRange(order);
    }

    public List<FoodNode> CompareFood(List<Food> theFood)
    {
        var givenList = theFood;

        mistakes.Clear();
        foreach (FoodNode node in clientOrder)
        {
            bool check = false;
            foreach (Food food in givenList)
            {
                if (food.Type == node.FoodType && food.CookedLevel == node.CookLevel)
                {
                    givenList.Remove(food);
                    food.Delete();
                    check = true;
                    break;
                }
            }
            if (!check) { mistakes.Add(node); }
        }
        return mistakes;
    }
}
