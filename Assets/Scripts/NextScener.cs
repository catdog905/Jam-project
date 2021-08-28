using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Clicked(){
        SceneManager.LoadScene("testingScene",LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
