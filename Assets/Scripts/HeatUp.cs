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
}
