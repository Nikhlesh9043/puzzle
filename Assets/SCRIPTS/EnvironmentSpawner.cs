using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnvTheme
    {
        public string theme_name;
        public GameObject[] env_objects;
        public Color ground_color;
        public Color water_color;
    }

    public List<EnvTheme> ThemeList;
    private List<GameObject> activeEnvObjects;

    public GameObject platform11x11;
    public GameObject platform11x21;
    public GameObject colorPlane;
    public GameObject platformSpawnerBlock;

    private void Start()
    {
        activeEnvObjects = new List<GameObject>();
        SetTheme(PlayerPrefs.GetInt("CurrentEnv", 0));
    }


    public void SetTheme(int index)
    {
        //Clear activeObjects
        foreach (GameObject obj in activeEnvObjects)
        {
            Destroy(obj);
        }
        activeEnvObjects.Clear();

        //Assign new Objects
        for(int i =0; i<40; i++)
        {
            GameObject obj = Instantiate(ThemeList[index].env_objects[Random.Range(0, ThemeList[index].env_objects.Length)], gameObject.transform);
            activeEnvObjects.Add(obj);
        }

        platform11x11.GetComponent<Renderer>().material.color = ThemeList[index].ground_color;
        platform11x21.GetComponent<Renderer>().material.color = ThemeList[index].ground_color;
        colorPlane.GetComponent<Renderer>().material.color = ThemeList[index].ground_color;
        platformSpawnerBlock.GetComponent<Renderer>().material.color = ThemeList[index].ground_color;

        GameValues.CurrentThemeName = ThemeList[index].theme_name;
        GameValues.GroundColor = ThemeList[index].ground_color;
        GameValues.WaterColor = ThemeList[index].water_color;
    }

    public void SpawnThemeEnv(string size)
    {
        int index = 0;
        foreach (GameObject obj in activeEnvObjects)
        {
            obj.transform.position = GetRandomPosition(index, size);
            obj.GetComponent<Animator>().Play("EnvironmentPrefabSpawn", -1, 0f);
            index += 1;
        }
    }

    private Vector3 GetRandomPosition(int index, string size)
    {
        float x=0, y=1, z=0;

        switch (size)
        {
            case "small":
                if (index < 20)//Top Sction
                {
                    x = Random.Range(-15, 15);
                    z = Random.Range(7.5f, 20);
                }
                else if (index < 27)//Right Section
                {
                    x = Random.Range(7.5f, 10);
                    z = Random.Range(5.5f, -5.5f);
                }
                else if (index < 34)//Left Section
                {
                    x = Random.Range(-7.5f, -10);
                    z = Random.Range(5.5f, -5.5f);
                }
                else //Bottom Section
                {
                    x = Random.Range(-6, 6);
                    z = Random.Range(-7.5f, -10);
                }
                break;
            case "large":
                if (index < 20)//Top Sction
                {
                    x = Random.Range(-15, 15);
                    z = Random.Range(9.5f, 20);
                }
                else if (index < 27)//Right Section
                {
                    x = Random.Range(7.5f, 10);
                    z = Random.Range(8, -8);
                }
                else if (index < 34)//Left Section
                {
                    x = Random.Range(-7.5f, -10);
                    z = Random.Range(8, -8);
                }
                else //Bottom Section
                {
                    x = Random.Range(-6, 6);
                    z = Random.Range(-9.5f, -10);
                }
                break;
        }

        return new Vector3(x,y,z);
    }
}
