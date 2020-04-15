using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    //AIを使わないで　敵の移動は決まったルートに従って移動する
    public GameObject[] wayPointGos;//public Transform[] wayPoints;//移動ルートの点 
    public float speed = 0.2f;
    private List<Vector3> wayPoints = new List<Vector3>();
    private int index = 0;
    private void Start()
    {
        LoadAPath(wayPointGos[Random.Range(0, wayPointGos.Length)]);
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
    }

    private void OnTriggerEnter2D(Collider2D collision) //敵がpacmanを食う
    {
        if (collision.gameObject.name == "Pacman")
        {
            Destroy(collision.gameObject);
        }
    }
}

