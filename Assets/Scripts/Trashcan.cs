using UnityEngine;

public class Trashcan : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var food = collision.gameObject.GetComponent<Food>();
        if (food != null)
        {
            food.Delete();
        }
    }
}
