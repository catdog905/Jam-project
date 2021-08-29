using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float timer = 0;
    public float timeToWin = 10f;
    string textSaved = "";
    [SerializeField]
    private Text text1,text2;
    bool countin = false;
    void OnTriggerStay2D(Collider2D collider){
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name.StartsWith("PlayerCharacter")){
            timer = 0f;
            countin=true;
            textSaved = text1.text;
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.name.StartsWith("PlayerCharacter")){
            text1.text=text2.text=textSaved;
            countin=false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (countin){
            timer += Time.deltaTime;
            if (timer >= timeToWin){
                text1.text=text2.text = "YOU WON";
                SceneManager.LoadScene("Won",LoadSceneMode.Single);
                text1=text2=null;
                return;
            }
            if (text1 != null)
                text1.text=text2.text = timer.ToString();
        }
    }
}
