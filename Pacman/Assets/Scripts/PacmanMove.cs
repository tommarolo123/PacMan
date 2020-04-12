
using UnityEngine;

public class PacmanMove : MonoBehaviour
{

    public float speed = 0.35f;//pacman移動スピード
    private Vector2 dest = Vector2.zero;//次の移動位置

    private void Start()
    {
        dest = transform.position;//初期位置　最初動かないように

    }
    private void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);//移動
        GetComponent<Rigidbody2D>().MovePosition(temp);//Rigidbodyから位置を設定
        if ((Vector2)transform.position == dest)  //移動間隔を固定する　前のdest位置に達成したらつぎのキー押す動作が効く
        {

            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Valid(Vector2.up)) //　　　　　　　　　　　　　　**Input.getAxis
            {
                dest = (Vector2)transform.position + Vector2.up;//　　　　Vector2型に変える　　　z座標いらない
             
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.right))
            {

                dest = (Vector2)transform.position + Vector2.right;
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))&& Valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
            }
        }
        }


    private bool Valid(Vector2 dir) // read me で説明
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}



