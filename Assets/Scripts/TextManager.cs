using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System;

public class TextManager : MonoBehaviour
{
    static public TextManager Instance;
    private ClientData client;
    [SerializeField] private GameObject textBox;

    [SerializeField] private float textSpeed;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private Image clienSprite;
    private string[] lines = new string[4];
    public string[] Lines { set { lines = value; } }

    private int index;
    private bool finished;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
    }

    public void SetClient(ClientData client)
    {
        this.client = client;
        textSpeed = client.VoiceSpeed;
        lines = new string[client.Dialogue.Count];
        for(int i = 0; i < client.Dialogue.Count; i++)
        {
            lines[i] = client.Dialogue[i];
        }
        nameBox.text = client.ClientName;
        clienSprite.sprite = client.Sprite;
    }

    public void StartText()
    {
        if (client != null)
        {
            dialogueBox.text = null;
            index = 0;
            textBox.SetActive(true);
            StartCoroutine(TypeText());
        }
    }

    IEnumerator TypeText()
    {
        finished = false;
        foreach (var character in lines[index].ToCharArray())
        {
            if (!finished)
            {
                dialogueBox.text += character;
                yield return new WaitForSeconds(textSpeed);
            }
            else { break; }
        }
        finished = true;
    }

    public void NextLine()
    {
        if (finished)
        {
            if (index < lines.Length - 1)
            {
                index++;
                dialogueBox.text = null;
                StartCoroutine(TypeText());
            }
            else
            {
                index = 0;
                dialogueBox.text = null;
                textBox.SetActive(false);
            }
        }
        else
        {
            finished = true;
            dialogueBox.text = lines[index];
        }
    }
}
