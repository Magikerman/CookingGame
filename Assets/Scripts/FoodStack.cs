using System.Collections.Generic;
using UnityEngine;

public class FoodStack : MonoBehaviour
{
    private List<FoodStack> foodStack = new List<FoodStack>();

    [SerializeField] private FoodStack root;
    public FoodStack Root { get { return root; } set { root = value; } }

    public List<FoodStack> FoodStacks { get { return foodStack; } set { foodStack = value; } }

    [SerializeField] private Transform setStackPosition;
    public Transform SetStackPosition => setStackPosition;


    private void Awake()
    {
        root = this;
    }

    public bool AddToStack(FoodStack thing)
    {
        bool finished = false;

        //setup list
        if (foodStack.Count <= 0)
        {
            foodStack.Add(this);
            root = this;
        }

        //position food
        thing.gameObject.transform.parent = null;
        thing.gameObject.transform.parent = foodStack[foodStack.Count - 1].SetStackPosition;
        thing.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        if (foodStack.Count == 1)
        {
            thing.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else { thing.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0); }
        foodStack.Add(thing);

        //arrange list
        foreach (var food in foodStack)
        {
            food.FoodStacks = foodStack;
        }
        thing.Root = this;

        return finished;
    }

    public void RemoveFromStack()
    {
        root = this;
        foodStack = new List<FoodStack>();
    }
}
