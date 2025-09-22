using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ClientData : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private string clientName;
    [SerializeField] private float voiceSpeed;
    [SerializeField] private List<FoodNode> order;
    [TextArea]
    [SerializeField] private List<string> dialogue;
    [SerializeField] private GameObject blood;

    public Sprite Sprite => sprite;
    public string ClientName => clientName;
    public float VoiceSpeed => voiceSpeed;
    public List<string> Dialogue => dialogue;

    //temp
    private void Start()
    {
        TellOrderToTheManager();
        foreach(var food in order)
        {
            dialogue.Add(food.CookLevel + " " + food.FoodType);
        }
        TextManager.Instance.SetClient(this);
    }

    public void Die()
    {
        Instantiate(blood, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Grab")) { Die(); }
    }

    private void TellOrderToTheManager()
    {
        FoodCompareManager.manager.SetClientOrder(order);
    }
}
