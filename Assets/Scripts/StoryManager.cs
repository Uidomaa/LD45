using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;
    public static int dialogueIndex = -1;

    [SerializeField]
    private GameObject dialogueCanvas;
    [SerializeField]
    private TextMeshProUGUI dialogueTMP;

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
        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDialogue ()
    {
        dialogueIndex++;
        dialogueCanvas.SetActive(true);
        dialogueTMP.text = "";
        switch (dialogueIndex)
        {
            case 0:
                dialogueTMP.DOText("Finally moved in to my own house!", 1f).SetEase(Ease.InSine);
                break;
            case 1:
                dialogueTMP.DOText("Now the first order of business is to get a kitty!", 1f).SetEase(Ease.InSine);
                break;
            case 2:
                dialogueTMP.DOText("I wonder where I get one...?", 2f).SetEase(Ease.InSine);
                break;
            case 3:
                dialogueTMP.DOText("...?", 1f).SetEase(Ease.InSine);
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(1);// Zoom into crystal ball
                break;
            default:
                dialogueCanvas.SetActive(false);//Close dialogue box
                break;
        }
    }

    public void CloseDialogue ()
    {
        dialogueCanvas.SetActive(false);
    }
}
