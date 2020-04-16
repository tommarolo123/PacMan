
using UnityEngine;
using System.Collections.Generic;
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

    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };

    private void Awake()
    {
        int tempCount = rawIndex.Count;
        _instance = this;
        for(int i = 0; i < tempCount; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }
    }

}
