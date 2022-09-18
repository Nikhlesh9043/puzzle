using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameValues
{
    private static string currentThemeName;
    private static Color groundColor;
    private static Color waterColor;


    public static string CurrentThemeName
    {
        get
        {
            return currentThemeName;
        }
        set
        {
            currentThemeName = value;
        }
    }

    public static Color GroundColor
    {
        get
        {
            return groundColor;
        }
        set
        {
            groundColor = value;
        }
    }

    public static Color WaterColor
    {
        get
        {
            return waterColor;
        }
        set
        {
            waterColor = value;
        }
    }
}
