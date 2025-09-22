using UnityEngine;

public class HeatUp : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var food = other.gameObject.GetComponent<Food>();
        if (food != null)
        {
            food.heatUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var food = other.gameObject.GetComponent<Food>();
        if (food != null)
        {
            food.Cooking = false;
        }
    }
}
