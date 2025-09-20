using UnityEngine;

public interface ITouchable
{
    public bool ObjectNeeded { get; }
    public void Interact(GameObject player);
}
