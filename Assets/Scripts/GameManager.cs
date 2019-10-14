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
    private RoomScriptableObject roomData;
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
            case GameState.gameStarting:
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

    private IEnumerator FadedLevelLoad(float fadeLength)
    {
        FadeOut(fadeLength);
        yield return new WaitForSeconds(fadeLength);
        SceneManager.LoadScene(roomData.roomNames[roomData.currentRoom]);
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

    public void LoadLevel (float fadeDuration = 3f)
    {
        StartCoroutine(FadedLevelLoad(fadeDuration));
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
                roomData.currentRoom = 0;
                FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(0);// Intro credits
                StoryManager.instance.shouldNotAdvance = true;
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
        //Tell player ghost was caught
        if (player.CollectedCatGhost() < roomData.numGhostsInRoom[roomData.currentRoom])
            return;
        //End level if all ghosts caught
        numCatsCollected += roomData.numGhostsInRoom[roomData.currentRoom];
        StoryManager.instance.CollectedGhostsDialogue();
        FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(1);// Zoom in on witch
        LoadScene(0, 5f);
    }

    private void OnLevelWasLoaded(int level)
    {
        switch (curGameState)
        {
            case GameState.gameStarting:
                //Loading level
                StoryManager.instance.CloseDialogue();
                SetGameState(GameState.gameAlive);
                break;
            case GameState.gameDead:
                //Failed level
                SetGameState(GameState.homeDead);
                break;
            case GameState.gameAlive:
                //Beat level
                roomData.currentRoom++;
                SetGameState(GameState.homeAlive);
                break;
        }
        FadeIn();
    }
}
