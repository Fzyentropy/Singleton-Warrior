using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public static int currentLevel = 0;
    public static int gameObjectArray;
    private const int highestLevel = 9;             // 总共关卡数：10
    private static bool isLoadingLevel = false;

    public TextMeshPro screenText;

    
    private void Awake()
    {
        isLoadingLevel = false;             // 加载关卡锁解开
        Time.timeScale = 1;                // 

        currentLevel = SceneManager.GetActiveScene().buildIndex;        //让 currentlevel 值等于当前场景编号
        
        gameObjectArray = GameObject.FindGameObjectsWithTag("Player").Length;        // initialize gameobject array
        Debug.Log("at awake, gameobjectarray:"+gameObjectArray);
        
        Debug.Log("level"+currentLevel+"awaked" );

        
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    
    void Start()
    {
        if (currentLevel == 0)
        {
            // 加载开始
        }

        // 第一关：加载叙事，出现level number
        // 其他关：出现 Level number
    }


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(GameManager.currentLevel, LoadSceneMode.Single);
        }

        
        if (gameObjectArray <= 1)
        {
            
            if ((currentLevel < highestLevel)&(!isLoadingLevel))            // 不是最后一关 且 没在加载关卡
            {
                isLoadingLevel = true;
                Debug.Log("is loading level:"+ isLoadingLevel);
                currentLevel++;
                SceneManager.LoadScene(GameManager.currentLevel, LoadSceneMode.Single);
            }

            if ((currentLevel == highestLevel)&(!isLoadingLevel))        // 最后一关
            {
                // 游戏胜利
                isLoadingLevel = true;
                Time.timeScale = 0;
                
                screenText.text = "YOU WIN!";
                Debug.Log("shit" );
            }
        }
        
        
    }


    IEnumerator FadeIn()
    {
        yield return null;
    }
    
    IEnumerator FadeOut()
    {
        yield return null;
    }
    
    IEnumerator LoadText()
    {
        yield return null;
    }

    IEnumerator HideText()
    {
        yield return null;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
