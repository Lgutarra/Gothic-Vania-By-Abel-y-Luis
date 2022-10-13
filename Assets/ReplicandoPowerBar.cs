using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplicandoPowerBar : MonoBehaviour
{   

    private Slider mSliderP;

    private 
    // Start is called before the first frame update
    void Start()
    {
         mSliderP = transform.Find(
            "Fill"
        ).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        mSliderP.value = HeroController.HeroInstance.mSliderPowerBar.value;
    }
}
