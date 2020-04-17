
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public GameObject pacman;
    public GameObject Clyde;
    public GameObject Inky;
    public GameObject Pinky;
    public GameObject Blinky;
    public bool isSuperPacman = false;


    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    public List<GameObject> pacdotGos = new List<GameObject>();
    private void Awake()
    {
        int tempCount = rawIndex.Count;
        _instance = this;
        for (int i = 0; i < tempCount; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }

        foreach (Transform t in GameObject.Find("Maze").transform)
        {
            pacdotGos.Add(t.gameObject);
        }
    }

    private void Start()
    {
        Invoke("CreatSuperPacdot", 10f);//10秒後、スーパー豆が出る

    }
    public void OnEatPacdot(GameObject go)
    {
        pacdotGos.Remove(go);
    }

    public void OnEatSuperPacdot()
    {
        Invoke("CreatSuperPacdot", 10f);
        isSuperPacman = true;
        FreezeEnemy();
        StartCoroutine(RecoveryEnemy());
    }

    IEnumerator RecoveryEnemy()
    {
        yield return new WaitForSeconds(3f);
        DisFreezeEnemy();
        isSuperPacman = false;
    }

    private void CreatSuperPacdot()
    {
        int tempIndex = Random.Range(0, pacdotGos.Count);//すべての豆からランダムに一つのスーパー豆を生成
        pacdotGos[tempIndex].transform.localScale = new Vector3(3, 3, 3);//スーパー豆を大きくする
        pacdotGos[tempIndex].GetComponent<Pacdot>().isSuperPacdot = true;
    }
    private void FreezeEnemy()
    {
        Blinky.GetComponent<GhostMove>().enabled = false;//updataが実行されなくなる　敵がとまる
        Clyde.GetComponent<GhostMove>().enabled = false;
        Pinky.GetComponent<GhostMove>().enabled = false;
        Inky.GetComponent<GhostMove>().enabled = false;
        Blinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);//色が透明にする
        Clyde.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        Pinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        Inky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }


    private void DisFreezeEnemy()
    {
        Blinky.GetComponent<GhostMove>().enabled = true;//敵の状態に戻る
        Clyde.GetComponent<GhostMove>().enabled = true;
        Pinky.GetComponent<GhostMove>().enabled = true;
        Inky.GetComponent<GhostMove>().enabled = true;
        Blinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        Clyde.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        Pinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        Inky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

    }
}