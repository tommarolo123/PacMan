using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{

    public bool isSuperPacdot = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Pacman")
        {
            Destroy(gameObject);
        }
    }
    
}
