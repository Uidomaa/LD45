﻿using System.Collections;
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
    public GameObject tinyRoom;

    [HideInInspector]
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
        continueText.SetActive(false);
        titleContinue.SetActive(false);
        tinyRoom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                dialogueTMP.DOText("I have finally moved in to my own place!", 1f).SetEase(Ease.InSine);
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
                GameManager.instance.LoadScene(1);//Load first level
                GameManager.instance.SetGameState(GameManager.GameState.gameAlive);
                //Reset index
                dialogueIndex = -1;
                break;
        }
        StartCoroutine(StoryCooldown(cooldownDuration));
    }

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
                dialogueTMP.DOText("I can cast magic with SPACEBAR. That should do the trick", 1f).SetEase(Ease.InSine);
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(2);// Zoom into crystal ball
                ShowTinyRoom();
                cooldownDuration = 2.5f;
                break;
            case 3:
                dialogueTMP.DOText("Ready or not, Kitty-souls, I'm coming for you!", 1f).SetEase(Ease.InSine);
                break;
            default:
                dialogueCanvas.SetActive(false);//Close dialogue box
                GameManager.instance.LoadScene(1);//Load first level
                GameManager.instance.SetGameState(GameManager.GameState.gameAlive);
                //Reset index
                dialogueIndex = -1;
                break;
        }
        StartCoroutine(StoryCooldown(cooldownDuration));
    }
    private void ShowTinyRoom ()
    {
        tinyRoom.SetActive(true);
        tinyRoom.transform.DOScale(0f, 1f).From().SetEase(Ease.OutBounce);
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
