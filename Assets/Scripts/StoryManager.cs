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
    public GameObject continueText;
    public GameObject titleContinue;
    
    [HideInInspector]
    public bool shouldNotAdvance = false;

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
        continueText.SetActive(false);
        titleContinue.SetActive(false);
    }

#if UNITY_EDITOR
    private void Update()
    {
        //DEBUG
        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogueIndex = -1;
            GameManager.instance.SetGameState(GameManager.GameState.homeWon);
        }
    }
#endif

    public void NextIntroDialogue ()
    {
        if (shouldNotAdvance)
            return;
        dialogueIndex++;
        dialogueCanvas.SetActive(true);
        dialogueTMP.text = "";
        float cooldownDuration = 1.5f;
        continueText.SetActive(false);
        switch (dialogueIndex)
        {
            case 0:
                dialogueTMP.DOText("Finally moved into my own place!", 1f).SetEase(Ease.InSine);
                break;
            case 1:
                dialogueTMP.DOText("The first order of business is to get a kitty!", 1f).SetEase(Ease.InSine);
                break;
            case 2:
                dialogueTMP.DOText("Now...how do I get one...?", 1f).SetEase(Ease.InSine);
                break;
            case 3:
                dialogueTMP.DOText("Of course!", 0.5f).SetEase(Ease.InSine);
                break;
            case 4:
                dialogueTMP.DOText("I just need to collect 9 cat souls!", 1f).SetEase(Ease.InSine);
                break;
            case 5:
                dialogueTMP.DOText("But where to find them?", 1f).SetEase(Ease.InSine);
                break;
            case 6:
                dialogueTMP.DOText("...!", 1f).SetEase(Ease.InSine);
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(2);// Zoom into crystal ball
                ShowTinyRoom();
                cooldownDuration = 2.5f;
                break;
            case 7:
                dialogueTMP.DOText("Ah! Thank you, Crystal Ball.", 1f).SetEase(Ease.InSine);
                break;
            case 8:
                dialogueTMP.DOText("An abandoned house would be purr-fect! Teehee!", 1f).SetEase(Ease.InSine);
                break;
            default:
                dialogueCanvas.SetActive(false);//Close dialogue box
                continueText.SetActive(false);
                GameManager.instance.LoadLevel();//Load first level
                GameManager.instance.SetGameState(GameManager.GameState.gameStarting);
                //Reset index
                dialogueIndex = -1;
                break;
        }
        StartCoroutine(StoryCooldown(cooldownDuration));
    }

    //Dialogue when player comes back afer dying
    public void NextDeathDialogue()
    {
        if (shouldNotAdvance)
            return;
        dialogueIndex++;
        dialogueCanvas.SetActive(true);
        dialogueTMP.text = "";
        float cooldownDuration = 1.5f;
        continueText.SetActive(false);
        switch (dialogueIndex)
        {
            case 0:
                dialogueTMP.DOText("Oh my!", 1f).SetEase(Ease.InSine);
                break;
            case 1:
                dialogueTMP.DOText("I should avoid touching the souls before weakening them!", 1f).SetEase(Ease.InSine);
                break;
            case 2:
                dialogueTMP.DOText("I can cast magic with SPACEBAR. That should do the trick.", 1f).SetEase(Ease.InSine);
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(2);// Zoom into crystal ball
                ShowTinyRoom();
                cooldownDuration = 2.5f;
                break;
            case 3:
                dialogueTMP.DOText("Ready or not, Kitty-souls, I'm coming for you!", 1f).SetEase(Ease.InSine);
                break;
            default:
                dialogueCanvas.SetActive(false);//Close dialogue box
                continueText.SetActive(false);
                GameManager.instance.LoadLevel();//Load same level
                GameManager.instance.SetGameState(GameManager.GameState.gameStarting);
                //Reset index
                dialogueIndex = -1;
                break;
        }
        StartCoroutine(StoryCooldown(cooldownDuration));
    }

    //Dialogue after player comes back with souls
    public void NextSuccessDialogue()
    {
        if (shouldNotAdvance)
            return;
        dialogueIndex++;
        dialogueCanvas.SetActive(true);
        dialogueTMP.text = "";
        float cooldownDuration = 1.5f;
        continueText.SetActive(false);
        string numCollectedSes = GameManager.instance.GetCatSoulsCollected() == 1 ? "" : "s";
        string numNextSes = GameManager.instance.roomData.numGhostsInRoom[GameManager.instance.roomData.currentRoom] == 1 ? "" : "s";
        switch (dialogueIndex)
        {
            case 0:
                dialogueTMP.DOText("I've collected " + GameManager.instance.GetCatSoulsCollected() + " soul" + numCollectedSes + "!", 1f).SetEase(Ease.InSine);
                break;
            case 1:
                dialogueTMP.DOText("Only " + (9 - GameManager.instance.GetCatSoulsCollected()) + " left to go!", 1f).SetEase(Ease.InSine);
                break;
            case 2:
                dialogueTMP.DOText("Looks like the next house has " + GameManager.instance.roomData.numGhostsInRoom[GameManager.instance.roomData.currentRoom] + " cat soul" + numNextSes + "!", 1f).SetEase(Ease.InSine);
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(2);// Zoom into crystal ball
                ShowTinyRoom();
                cooldownDuration = 2.5f;
                break;
            case 3:
                dialogueTMP.DOText("Ready or not, Kitty-soul" + numNextSes + ", I'm coming for you!", 1f).SetEase(Ease.InSine);
                break;
            default:
                dialogueCanvas.SetActive(false);//Close dialogue box
                continueText.SetActive(false);
                GameManager.instance.LoadLevel();//Load next level
                GameManager.instance.SetGameState(GameManager.GameState.gameStarting);
                //Reset index
                dialogueIndex = -1;
                break;
        }
        StartCoroutine(StoryCooldown(cooldownDuration));
    }

    /// <summary>
    /// Dialogue once player has collected all souls
    /// </summary>
    public void NextWinDialogue ()
    {
        if (shouldNotAdvance)
            return;
        dialogueIndex++;
        dialogueCanvas.SetActive(true);
        dialogueTMP.text = "";
        float cooldownDuration = 1.5f;
        continueText.SetActive(false);
        switch (dialogueIndex)
        {
            case 0:
                dialogueTMP.DOText("I've finally collected all 9 souls!!", 1f).SetEase(Ease.InSine);
                break;
            case 1:
                dialogueTMP.DOText("Time to make them into a full cat!", 1f).SetEase(Ease.InSine);
                break;
            case 2:
                dialogueTMP.DOText("........!!", 5f).SetEase(Ease.InSine);
                StartCoroutine(CatSpell.instance.DoCatSpell());
                cooldownDuration = 5f;
                break;
            case 3:
                dialogueTMP.DOText("It worked! A real cat!", 1f).SetEase(Ease.InSine);
                break;
            case 4:
                dialogueTMP.DOText("Thanks so much for your help!", 1f).SetEase(Ease.InSine);
                break;
            case 5:
                dialogueTMP.DOText("You were great! Maybe I'll see you again some time?", 1f).SetEase(Ease.InSine);
                StartCoroutine(GoToEndCredits());
                cooldownDuration = 6f;
                break;
            default:
                dialogueCanvas.SetActive(false);//Close dialogue box
                GameManager.instance.SetGameState(GameManager.GameState.homeEndCredits);
                //Reset index
                dialogueIndex = -1;
                break;
        }
        StartCoroutine(StoryCooldown(cooldownDuration));
    }

    //Dialogue when player has collected all the souls in a level
    public void CollectedGhostsDialogue()
    {
        dialogueCanvas.SetActive(true);
        dialogueTMP.text = "";
        dialogueTMP.DOText("Yay! I've collected more cat souls!", 1f).SetEase(Ease.InSine);
        continueText.SetActive(false);
    }

    private void ShowTinyRoom ()
    {
        StartCoroutine(CrystalBallScript.instance.ShowTinyRoom());
    }

    private IEnumerator GoToEndCredits ()
    {
        GameManager.instance.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(0);// Credits
        CatSpell.instance.ShowCreditsCat();
        yield return new WaitForSeconds(3f);
        GameManager.instance.FadeIn(2f);
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator StoryCooldown (float cooldownDuration = 1f)
    {
        shouldNotAdvance = true;
        yield return new WaitForSeconds(cooldownDuration);
        shouldNotAdvance = false;
        continueText.SetActive(true);
    }

    public void CloseDialogue ()
    {
        dialogueCanvas.SetActive(false);
    }
}
