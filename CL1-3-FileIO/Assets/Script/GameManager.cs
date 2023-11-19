using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public static int currentLevel = 0;
    public static int gameObjectArray;
    private const int highestLevel = 7;             // 总共关卡数：8
    private static bool isLoadingLevel = false;
    public static bool isMovable = true;          // 是否可操作

    public GameObject blackScreen;
    public GameObject txt;
    public TMP_Text txtText;
    public GameObject test;

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
                StartCoroutine(StoryEnd());
                
                Debug.Log("shit" );
            }
        }
        
        
    }


    IEnumerator StoryStart()
    {
        isMovable = false;
        
        PATH_STORY_START = Application.dataPath + DIR_DATA + FILE_STORY_START;
        
        if (blackScreen == null)
        {
            blackScreen = GameObject.Find("Black");
        }
        if (txt == null)
        {
            txt = GameObject.Find("TXT");
        }
        txtText = txt.GetComponent<TMP_Text>();

        
        // file is here. file I/O

        txtText.text = "";
        txtText.color = new Color(1,1,1,0);
        // txtText.text = File.ReadAllText(PATH_STORY_START);
        txtText.text =
            "In the kingdom of multiton, everyone has multiple selves, they are able to play multiple roles in their multiple lives, their time are multiplied. However, one day, Matt the Singleton Demon came to this kingdom. He forced everyone in the kingdom to singletonize their multiple selves and became singleton. This brought great disaster to them. A Multi-Worrier decides to defeat the Singleton Demon and save the kingdom. He puts on his yellow armor, and comes to demon's realm. First of all, he has to singleton himselves to face the demon.....";
        
        // black screen is here
        
        if (!blackScreen.activeSelf)
        {
            blackScreen.SetActive(true);
        }
        
        blackScreen.GetComponent<RawImage>().color = Color.black;

        
        // text fade in
        
        float textFadeSpeed = 0.05f;
        float textFadeTime = 0.04f;
        
        
        // 尝试解决 WebGL bug
        
        /*float textFadeDuration = 1f;
        float elapsedTime = 0;
        Color transparentColor = new Color(1, 1, 1, 0); // 完全透明的白色
        Color whiteColor = new Color(1, 1, 1, 1); // 不透明的白色

        // 开始时文本完全透明
        txtText.color = transparentColor;
        
        Instantiate(test, new Vector3(0, 0, 0), Quaternion.identity);

        while (txtText.color.a <= 0.95f)
        {
            txtText.color = new Color(1, 1, 1, txtText.color.a + textFadeSpeed);
            yield return new WaitForSeconds(textFadeTime);
        }

        // 确保淡入结束时文本是不透明的白色
        txtText.color = whiteColor;
        
        Instantiate(test, new Vector3(0, 0, 0), Quaternion.identity);*/
        
        
        while (true)
        {
            if (txt.GetComponent<TMP_Text>().color.a >= 1)
            { Debug.Log("break"); break; }

            txt.GetComponent<TMP_Text>().color = new Color(1,1,1,txt.GetComponent<TMP_Text>().color.a + textFadeSpeed);
            Debug.Log(txt.GetComponent<TMP_Text>().color.a);

            yield return new WaitForSeconds(textFadeTime);
            
        }
        
        
        
        // last 10 secs

        yield return new WaitForSeconds(11.53f);
        
        
        // text fade out
        // black screen gone

        blackScreen.SetActive(false);
        txt.SetActive(false);

        isMovable = true;
        
    }

    
    
    IEnumerator StoryEnd()
    {
        isMovable = false;
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        
        // PATH_STORY_END = Application.dataPath + DIR_DATA + FILE_STORY_END;
        
        if (blackScreen == null)
        {
            blackScreen = GameObject.Find("Black");
        }
        if (txt == null)
        {
            txt = GameObject.Find("TXT");
        }
        txtText = txt.GetComponent<TMP_Text>();
        

        if(!blackScreen.activeSelf) {blackScreen.SetActive(true);}
        blackScreen.GetComponent<RawImage>().color = Color.clear;
        
        if(!txt.activeSelf){txt.SetActive(true);}
        txtText.text = "";
        txtText.color = new Color(1,1,1,0);
        // txtText.text = File.ReadAllText(PATH_STORY_END);
        txtText.text =
            "Passed by the maze in the demon's realm, the Multi-Worrier finally faces Matt the Singleton Demon himself. However, the worrier lost too much power during singletoning himselves. Matt uses his Ultra Power of WASDController and gives the worrier the final punch. The worrier is beaten down on the ground..... After the failure of the worrier, no one can stop Matt singletoning the world any more.....";
        
        // black fade in
        
        float blackFadeSpeed = 0.1f;
        float blackFadeTime = 0.1f;

        while (true)
        {
            if (blackScreen.GetComponent<RawImage>().color.a >= 1)
            { Debug.Log("break"); break; }

            blackScreen.GetComponent<RawImage>().color = new Color(0,0,0,blackScreen.GetComponent<RawImage>().color.a + blackFadeSpeed);
            Debug.Log("waht");

            yield return new WaitForSeconds(blackFadeTime);
            
        }
        
        // last 2 secs

        yield return new WaitForSeconds(2f);

        // text fade in 

        
        
        float textFadeSpeed = 0.05f;
        float textFadeTime = 0.04f;

        while (true)
        {
            if (txt.GetComponent<TMP_Text>().color.a >= 1)
            { Debug.Log("break"); break; }

            txt.GetComponent<TMP_Text>().color = new Color(1,1,1,txt.GetComponent<TMP_Text>().color.a + textFadeSpeed);
            Debug.Log("waht");

            yield return new WaitForSeconds(textFadeTime);
            
        }
        
    }
    
    
    
    
    
    
}
