using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalBallScript : MonoBehaviour
{
    public static CrystalBallScript instance;

    public GameObject tinyRoom;
    public GameObject[] catGhosts;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tinyRoom.SetActive(false);
        for (int i = 0; i < catGhosts.Length; i++)
            catGhosts[i].SetActive(false);
    }

    public IEnumerator ShowTinyRoom ()
    {
        //Show number of ghosts in next room
        int numGhostsInNextRoom = GameManager.instance.roomData.numGhostsInRoom[GameManager.instance.roomData.currentRoom];
        for (int i = 0; i < catGhosts.Length; i++)
            catGhosts[i].SetActive(i < numGhostsInNextRoom ? true : false);
        yield return transform.DOPunchPosition(Vector3.up * 0.05f, 0.75f, 10, 0).WaitForCompletion();
        tinyRoom.SetActive(true);
        tinyRoom.transform.DOScale(0f, 1f).From().SetEase(Ease.OutBounce);
    }

    public void HideCrystalBall ()
    {
        gameObject.SetActive(false);
    }
}
