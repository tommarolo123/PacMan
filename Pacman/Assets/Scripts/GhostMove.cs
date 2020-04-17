using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    //AIを使わないで　敵の移動は決まったルートに従って移動する
    public GameObject[] wayPointGos;//public Transform[] wayPoints;//移動ルートの点 
    public float speed = 0.2f;
    private List<Vector3> wayPoints = new List<Vector3>();
    private int index = 0;
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position + new Vector3(0, 3, 0); //敵の出発点　初期位置上のｙ＋３  
        LoadAPath(wayPointGos[GameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder - 2]]);//敵が2から5層　-2すると0から3になる。usingIndex[]に引き渡す。その番号を用いてwayPointGosのルールを選ぶ
        
    }

    private void FixedUpdate()
    {
        if (transform.position != wayPoints[index])
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, wayPoints[index], speed);
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
        else
        {
            index++;
            
            if(index >= wayPoints.Count)
            {
                index = 0;
                LoadAPath(wayPointGos[Random.Range(0, wayPointGos.Length)]);
            }
        }
        Vector2 dir = (Vector2)wayPoints[index] - (Vector2)transform.position; //移動方向ベクトルを取得してanimatorに渡す
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    
    }
    private void LoadAPath(GameObject go)
    {

        wayPoints.Clear();
        foreach (Transform t in go.transform)   //[a,b)
        {
            wayPoints.Add(t.position);
        }
        wayPoints.Insert(0, startPos);//それぞれの敵の出発点違うようにする
            wayPoints.Add(startPos);
       
    }

    private void OnTriggerEnter2D(Collider2D collision) //敵がpacmanを食う
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (GameManager.Instance.isSuperPacman) //食えないで、敵が原点に戻る

            {
                transform.position = startPos - new Vector3(0, 3, 0);//原点に戻る
                index = 0;
            }
            else
            {
                Destroy(collision.gameObject);
            }
            }
    }
}

