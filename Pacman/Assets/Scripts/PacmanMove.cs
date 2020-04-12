
using UnityEngine;

public class PacmanMove : MonoBehaviour
{

    public float speed = 0.35f;//pacman移動スピード
    private Vector2 dest = Vector2.zero;//次の移動位置

    private void Start()
    {
        dest = transform.position;//初期位置　最初動かないように

    }

}
