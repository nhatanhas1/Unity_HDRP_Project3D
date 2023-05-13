using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemScene : MonoBehaviour
{
    public enum SceneIndexs
    {
        StartScene =1,
        GameScene =2,
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SystemScene");
        SceneManager.LoadScene((int)SceneIndexs.StartScene, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {

    }
}
