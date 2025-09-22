using UnityEngine;

public class SetFoodList : MonoBehaviour
{
    [SerializeField] private BellRing bell;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>() != null)
        {
            bell.AddToList(other.GetComponent<Food>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Food>() != null)
        {
            bell.RemoveFromList(other.GetComponent<Food>());
        }
    }
}
