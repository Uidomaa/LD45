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

    private int numCats = 0;

    private GameState curGameState = GameState.menu;
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
        titleImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (curGameState)
        {
            case GameState.menu:
                //TODO Fade title image
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    titleImage.DOFade(0f, 2f).SetEase(Ease.InOutSine).OnComplete( () => { StoryManager.instance.shouldNotAdvance = false; });
                    SetGameState(GameState.home);
                }
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

    private IEnumerator FadedSceneLoad (int sceneToLoad)
    {
        FadeOut(3f);
        yield return new WaitForSeconds(3f);
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

    public void LoadScene (int sceneToLoad)
    {
        StartCoroutine(FadedSceneLoad(sceneToLoad));
    }

    public GameState GetGameState ()
    {
        return curGameState;
    }

    public void SetGameState(GameState newGameState)
    {
        curGameState = newGameState;
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
