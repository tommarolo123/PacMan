using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public GameObject startCountDownPrefab;
    public GameObject gameoverPrefab;
    public GameObject winPrefab;
    public AudioClip startClip;
    public GameObject startPanel;
    public GameObject gamePanel;

    private int pacdotNum = 0;//全部の豆数
    private int nowEat = 0;//今食べた豆数
    public int score = 0;//

    public Text remainText;
    public Text nowText;
    public Text scoreText;

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
        pacdotNum = GameObject.Find("Maze").transform.childCount;
    
    }

    private void Start()

    {
        SetGameState(false);
      

    }

    private void Update()
    {
        if(nowEat == pacdotNum&&pacman.GetComponent<PacmanMove>().enabled != false)
        {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            SetGameState(false);
        }
        if(nowEat == pacdotNum && pacman)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (gamePanel.activeInHierarchy)
        {
            remainText.text = "Remain:\n\n" + (pacdotNum - nowEat);
            nowText.text = "Eaten:\n\n" + nowEat;
            scoreText.text = "Score:\n\n" + score;
        }
    }
    public void OnstartButton()
    {
        StartCoroutine(PlayStartCountDown());
        AudioSource.PlayClipAtPoint(startClip, new Vector3(0,0,-10));
        
        startPanel.SetActive(false);


    }

   IEnumerator PlayStartCountDown()
    {
        
        GameObject go = Instantiate(startCountDownPrefab);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);
        Invoke("CreatSuperPacdot", 10f);
        gamePanel.SetActive(true);
        GetComponent<AudioSource>().Play();

    }
    public void OnExitButton()
    {
        Application.Quit();
    }
    public void OnEatPacdot(GameObject go)
    {
        nowEat++;
        score += 100;
        pacdotGos.Remove(go);

    }

    public void OnEatSuperPacdot() { 
        score += 200;//スーパー豆は300点
        score += 200;//スーパー豆は300点
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
        if (pacdotGos.Count < 5)
        {
            return;
        }
        int tempIndex = Random.Range(0, pacdotGos.Count);//すべての豆からランダムに一つのスーパー豆を生成
        pacdotGos[tempIndex].transform.localScale = new Vector3(4, 4, 4);//スーパー豆を大きくする
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
    private void SetGameState(bool state)
    {
        Blinky.GetComponent<GhostMove>().enabled = state;
        Clyde.GetComponent<GhostMove>().enabled = state;
        Pinky.GetComponent<GhostMove>().enabled = state;
        Inky.GetComponent<GhostMove>().enabled = state;
        pacman.GetComponent<PacmanMove>().enabled = state;
    
    }

}