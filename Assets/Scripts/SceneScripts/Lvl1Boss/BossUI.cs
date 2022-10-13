using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{   

    public GameObject bossPanel;
    public GameObject walls;

    public GameObject Boss_auxiliar;

    public static BossUI instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        bossPanel.SetActive(false);
        walls.SetActive(false);
        Boss_auxiliar.SetActive(false);
    }

    public void BossActivator()
    {
        bossPanel.SetActive(true);
        walls.SetActive(true);
        Boss_auxiliar.SetActive(true);
    }


    
    
}
