using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Heat : MonoBehaviour
{
    public float Timer = 5f;
    public int Cold = 100;
    public Slider slider;

    // Update is called once per frame
    void SetMaxHealth(){
        slider.maxValue = Cold;
        slider.value = Cold;
    }
    
    //main timer
    void Update()
    {
       Timer -= Time.deltaTime;
       if(Timer <= 0.0f){
            timerEnd();
       } 
    }

    //Activated once timer above reaches zero
    void timerEnd(){
        Cold -= 2;
        slider.value = Cold;
        Timer = 5f;
        if(Cold <= 0){
            SceneManager.LoadScene("GameOver");
        }
    }
}
