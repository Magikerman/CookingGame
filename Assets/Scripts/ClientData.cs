using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ClientData : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private string clientName;
    [SerializeField] private float voiceSpeed;
    [SerializeField] private List<string> order;
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
}
