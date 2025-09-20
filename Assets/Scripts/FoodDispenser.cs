using UnityEngine;

public class FoodDispenser : MonoBehaviour , ITouchable
{
    [SerializeField] private bool objectNeeded;
    public bool ObjectNeeded { get { return objectNeeded; } }
    [SerializeField] private GameObject foodPrefab;
    public void Interact(GameObject player)
    {
        if (player.GetComponent<MichaelMovement>().ThingGrabbed == null)
        {
            player.GetComponent<MichaelMovement>().SendGrab(Instantiate(foodPrefab));
        }
    }
}
