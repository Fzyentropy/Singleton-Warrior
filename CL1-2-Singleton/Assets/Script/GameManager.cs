using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public static int currentLevel = 0;
    public static int gameObjectArray;
    private const int highestLevel = 9;             // 总共关卡数：10
    private static bool isLoadingLevel = false;

    public Component blackScreen;
    public GameObject txt;
    public float fadeSpeed = 1.5f;

    private const string DIR_DATA = "/Data/";
    private const string FILE_STORY_START = "Story_Start.txt";
    private const string FILE_STORY_END = "Story_End.txt";

    private string PATH_STORY_START;
    private string PATH_STORY_END;
    

    private void Awake()
    {
        
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        
        isLoadingLevel = false;             // 加载关卡锁解开
        Time.timeScale = 1;                // 

        currentLevel = SceneManager.GetActiveScene().buildIndex;        //让 currentlevel 值等于当前场景编号
        
        gameObjectArray = GameObject.FindGameObjectsWithTag("Player").Length;        // initialize gameobject array
        Debug.Log("at awake, gameobjectarray:"+gameObjectArray);
        
        Debug.Log("level"+currentLevel+"awaked" );
        

        

        
        

    }


    
    void Start()
    {
        if (currentLevel == 0)
        {
            StartCoroutine(StoryStart());

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
                
                Debug.Log("shit" );
            }
        }
        
        
    }


    IEnumerator StoryStart()
    {
        Time.timeScale = 0;

        PATH_STORY_START = Application.dataPath + DIR_DATA + FILE_STORY_START;
        PATH_STORY_END = Application.dataPath + DIR_DATA + FILE_STORY_END;
        
        blackScreen = GameObject.Find("Black").GetComponent<RawImage>();
        txt = GameObject.Find("TXT");

        txt.GetComponent<TMP_Text>().text = File.ReadAllText(PATH_STORY_START);

        // black screen is here
        // text fade in
        
        // last 10 secs
        
        // text fade out
        // black screen gone

        Time.timeScale = 1;
        
        yield return null;
    }

    IEnumerator StoryEnd()
    {
        Time.timeScale = 0;
        
        // black fade in
        
        // last 2 secs
        
        // text fade in 

        yield return null;
    }

    
    IEnumerator TextFadeIn()
    {
        yield return null;
    }

    IEnumerator TextFadeOut()
    {
        yield return null;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
