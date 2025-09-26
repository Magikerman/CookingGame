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
        SetAsTextManagerCurrentClient();
    }

    public void Die()
    {
        Instantiate(blood, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //temp
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Grab")) { Die(); }
    }

    private void TellOrderToTheManager()
    {
        FoodCompareManager.manager.SetClientOrder(order);
    }

    private void SetAsTextManagerCurrentClient()
    {
        TextManager.Instance.SetClient(this);
    }
}
