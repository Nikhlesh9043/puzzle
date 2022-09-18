using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlatformSpawner : MonoBehaviour
{
    [System.Serializable]
    public class LevelInfo
    {
        public int index;
        public GameObject platform;
        public float playerX;
        public float playerZ;
        public float homeX;
        public float homeZ;
        public string size="small";
    }

    public List<LevelInfo> Levels;
    private LevelInfo currentLevel;
    public TextMeshProUGUI StageText;

    public GameObject MainMenu;
    public GameObject startBtn;

    public GameObject Player;
    public GameObject PlayerPrefab;
    public GameObject PlatformGraphics;
    public GameObject Home;

    public GameObject EndCanvas;

    private Animator platformAnimator;

    public int level;

    public EnvironmentSpawner envSpawnScript;
    public GameObject platform11x11;
    public GameObject platform11x21;


    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //Load saved Stage
        level = PlayerPrefs.GetInt("CurrentLevel",0);
    }

    // Start is called before the first frame update
    void Start()
    {
        platformAnimator = gameObject.GetComponent<Animator>();
        SpawnPlatform();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnPlatform()
    {
        switch (level)
        {
            case 0:
                envSpawnScript.SetTheme(0);
                PlayerPrefs.SetInt("CurrentEnv", 0);
                break;

            case 10:
                envSpawnScript.SetTheme(3);
                PlayerPrefs.SetInt("CurrentEnv", 3);
                break;

            case 20:
                envSpawnScript.SetTheme(1);
                PlayerPrefs.SetInt("CurrentEnv", 1);
                break;
            case 30:
                envSpawnScript.SetTheme(2);
                PlayerPrefs.SetInt("CurrentEnv", 2);
                break;
            case 40:
                envSpawnScript.SetTheme(4);
                PlayerPrefs.SetInt("CurrentEnv", 4);
                break;
            case 50:
                envSpawnScript.SetTheme(0);
                PlayerPrefs.SetInt("CurrentEnv", 0);
                break;
            case 60:
                envSpawnScript.SetTheme(3);
                PlayerPrefs.SetInt("CurrentEnv", 3);
                break;
            case 70:
                envSpawnScript.SetTheme(1);
                PlayerPrefs.SetInt("CurrentEnv", 1);
                break;
            case 80:
                envSpawnScript.SetTheme(2);
                PlayerPrefs.SetInt("CurrentEnv", 2);
                break;
            case 90:
                envSpawnScript.SetTheme(4);
                PlayerPrefs.SetInt("CurrentEnv", 4);
                break;
        }

        MainMenu.SetActive(true);
        currentLevel = Levels[level];
        GameObject Platform = Instantiate(currentLevel.platform, PlatformGraphics.transform);
        Home.transform.position = new Vector3(currentLevel.homeX, -0.4f, currentLevel.homeZ);

        platformAnimator.Play("SpawnPlatform", -1, 0f);
        StageText.text = "Stage " + (level+1);
        Invoke("ReadyToStart", 1.5f);

        //CustomMeshGenerator.UpdateMesh(currentLevel.size);
        if (currentLevel.size == "small")
        {
            platform11x11.SetActive(true);
            platform11x21.SetActive(false);
        }
        else
        {
            platform11x11.SetActive(false);
            platform11x21.SetActive(true);
        }
        envSpawnScript.SpawnThemeEnv(currentLevel.size);

        Platform.GetComponent<Renderer>().materials[0].color = GameValues.GroundColor;
        Platform.GetComponent<Renderer>().materials[1].color = GameValues.WaterColor;

        
    }

    public void ReadyToStart()
    {
        startBtn.SetActive(true);

        //To autostart level (not haveing to press continue)
        //MainMenu.GetComponent<Gameplay>().StartGame();
    }

    public void StartGame()
    {
        Player = Instantiate(PlayerPrefab, new Vector3(currentLevel.playerX, 0.4f, currentLevel.playerZ),Quaternion.identity);
        Player.SetActive(true);
    }

    public void LevelComplete()
    {
        Destroy(Player);
        platformAnimator.Play("CollapsePlatform", -1, 0f);
        Invoke("DelayedLevelComplete", 1f);

       
        PlayerPrefs.SetInt("CurrentLevel", (level + 1));
    }

    private void DelayedLevelComplete()
    {
        foreach (Transform item in PlatformGraphics.transform)
        {
            item.gameObject.SetActive(false);
        }

        level += 1;
        if (level > 99)
        {
            level = 0;
            PlayerPrefs.SetInt("CurrentLevel", 0);
            EndCanvas.SetActive(true);
        }

        SpawnPlatform();
    }
}
