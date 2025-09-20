using UnityEngine;

public class BellRing : MonoBehaviour , ITouchable
{
    [SerializeField] private bool objectNeeded;
    public bool ObjectNeeded { get { return objectNeeded; } }
    public void Interact(GameObject player)
    {
        TextManager.Instance.StartText();
    }
}
