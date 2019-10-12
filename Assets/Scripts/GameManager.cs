using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Image screenFader;
    public Image titleImage;

    private int numCatsCollected = 0;

    private GameState curGameState = GameState.intro;
    public enum GameState
    {
        intro,
        menu,
        homeIntro,
        gameStarting,
        gameAlive,
        gameDead,
        homeAlive,
        homeDead
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
        StartCoroutine(DoIntroTimeout());
    }

    // Update is called once per frame
    void Update()
    {
        switch (curGameState)
        {
            case GameState.intro:
                break;
            case GameState.menu:
                //TODO Fade title image
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    titleImage.DOFade(0f, 2f).SetEase(Ease.InOutSine).OnComplete(() =>
                   {
                       StoryManager.instance.shouldNotAdvance = false;
                       StoryManager.instance.NextIntroDialogue();
                   });
                    FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(1);// Show witch
                    SetGameState(GameState.homeIntro);
                    StoryManager.instance.titleContinue.SetActive(false);
                }
                break;
            case GameState.homeIntro:
                UpdateIntroDialogue();
                break;
            case GameState.gameAlive:
                break;
            case GameState.gameDead:
                break;
            case GameState.homeAlive:
                UpdateSuccessDialogue();
                break;
            case GameState.homeDead:
                break;
            default:
                Debug.LogError(curGameState + " not handled!");
                break;
        }
    }

    private IEnumerator DoIntroTimeout ()
    {
        FadeIn(10);
        SetGameState(GameState.intro);
        titleImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        StoryManager.instance.titleContinue.SetActive(true);
        SetGameState(GameState.menu);
    }

    private void UpdateIntroDialogue ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StoryManager.instance.NextIntroDialogue();
        }
    }

    private void UpdateSuccessDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StoryManager.instance.NextSuccessDialogue();
        }
    }

    private IEnumerator FadedSceneLoad (int sceneToLoad, float fadeLength)
    {
        FadeOut(fadeLength);
        yield return new WaitForSeconds(fadeLength);
        SceneManager.LoadScene(sceneToLoad);
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

    public void LoadScene (int sceneToLoad, float fadeDuration = 3f)
    {
        StartCoroutine(FadedSceneLoad(sceneToLoad, fadeDuration));
    }

    public GameState GetGameState ()
    {
        return curGameState;
    }

    public int GetCatSoulsCollected ()
    {
        return numCatsCollected;
    }

    public void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.intro:
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(0);// Intro credits
                break;
            case GameState.menu:
                break;
            case GameState.homeIntro:
                break;
            case GameState.gameStarting:
                break;
            case GameState.gameAlive:
                break;
            case GameState.gameDead:
                break;
            case GameState.homeAlive:
                StoryManager.instance.NextSuccessDialogue();
                break;
            case GameState.homeDead:
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(1);// Show witch
                break;
        }
        curGameState = newGameState;
    }

    public void PlayerDied ()
    {
        AudioManager.instance.PlayPlayerKilled();
        SetGameState(GameState.gameDead);
        LoadScene(0, 0.5f);
    }

    public void DefeatedGhost (PlayerController player)
    {
        numCatsCollected++;
        player.CollectedCatGhost();
        StoryManager.instance.CollectedGhostsDialogue();
        FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(1);// Zoom in on witch
        LoadScene(0, 5f);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (curGameState != GameState.homeIntro)
            StoryManager.instance.CloseDialogue();
        switch (curGameState)
        {
            case GameState.gameStarting:
                SetGameState(GameState.gameAlive);
                break;
            case GameState.gameDead:
                SetGameState(GameState.homeDead);
                break;
            case GameState.gameAlive:
                SetGameState(GameState.homeAlive);
                break;
        }
        FadeIn();
    }
}
