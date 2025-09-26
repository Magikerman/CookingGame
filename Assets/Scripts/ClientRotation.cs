using UnityEngine;

public class ClientRotation : MonoBehaviour
{
    [SerializeField] private Transform player;

    void FixedUpdate()
    {
        transform.LookAt(player);
    }
}
