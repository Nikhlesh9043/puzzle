using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static GameObject Player;
    public GameObject PLatformSpawner;
    public GameObject startBtn;

    private PlatformSpawner platformScript;
    // Start is called before the first frame update
    void Start()
    {
        platformScript = PLatformSpawner.GetComponent<PlatformSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        platformScript.StartGame();
        startBtn.SetActive(false);
        gameObject.SetActive(false);
    }
}
