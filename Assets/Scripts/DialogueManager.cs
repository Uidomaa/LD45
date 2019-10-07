using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDialogue (int dialogueIndex)
    {
        switch (dialogueIndex)
        {
            case 0:
                return "";
            default:
                Debug.LogError(dialogueIndex + " not valid dialogue index");
                return "";
        }
    }
}
