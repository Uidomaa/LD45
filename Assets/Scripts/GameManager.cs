using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Image screenFader;

    private int numCats = 0;

    private GameState curGameState = GameState.home;
    public enum GameState
    {
        menu,
        home,
        gameAlive,
        gameDead
    }

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
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        switch (curGameState)
        {
            case GameState.menu:
                break;
            case GameState.home:
                UpdateIntroDialogue();
                break;
            case GameState.gameAlive:
                break;
            case GameState.gameDead:
                break;
            default:
                Debug.LogError(curGameState + " not handled!");
                break;
        }
    }

    private void UpdateIntroDialogue ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StoryManager.instance.NextDialogue();
        }
    }

    private void FadeIn (float fadeTime = 3f)
    {
        screenFader.color = Color.black;
        screenFader.DOFade(0f, fadeTime).SetEase(Ease.InOutSine);
    }

    public void FadeOut (float fadeTime = 3f)
    {
        screenFader.DOFade(1f, fadeTime).SetEase(Ease.InOutSine);
    }

    public GameState GetGameState ()
    {
        return curGameState;
    }

    public void DefeatedGhost ()
    {
        Debug.Log("HIT!");
    }

    private void OnLevelWasLoaded(int level)
    {
        if (curGameState != GameState.home)
            StoryManager.instance.CloseDialogue();
        Debug.Log("Fading in");
        FadeIn();
    }
}
