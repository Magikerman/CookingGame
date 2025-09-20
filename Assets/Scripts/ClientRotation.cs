using UnityEngine;

public class ClientRotation : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.LookAt(player);
    }
}
