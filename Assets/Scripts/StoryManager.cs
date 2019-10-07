using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;
    public static int dialogueIndex = -1;

    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueTMP;
    public GameObject tinyRoom;

    public bool shouldNotAdvance = true;

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
        if (shouldNotAdvance)
            return;
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
                StartCoroutine(ShowTinyRoom());
                break;
            case 4:
                dialogueTMP.DOText("Of course!", 0.5f).SetEase(Ease.InSine);
                break;
            case 5:
                dialogueTMP.DOText("I just need to collect 9 cat souls!", 1f).SetEase(Ease.InSine);
                break;
            default:
                dialogueCanvas.SetActive(false);//Close dialogue box
                GameManager.instance.LoadScene(1);//Load first level
                GameManager.instance.SetGameState(GameManager.GameState.gameAlive);
                break;
        }
    }

    private IEnumerator ShowTinyRoom ()
    {
        shouldNotAdvance = true;
        tinyRoom.SetActive(true);
        yield return tinyRoom.transform.DOScale(0f, 1f).From().SetEase(Ease.OutBounce).WaitForCompletion();
        shouldNotAdvance = false;
    }

    public void CloseDialogue ()
    {
        dialogueCanvas.SetActive(false);
    }
}
