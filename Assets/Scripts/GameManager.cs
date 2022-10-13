using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public HeroController hero;

    public Boss boss;

    void Awake()
    {
        Instance = this;        
    }
}
