using UnityEngine;
using UnityEditor;

public static class Utilities
{
    public static bool IsOdd(int value)
    {
        return value % 2 != 0;
    }

    /// <summary>
    /// Converts HSV using [0, 255] to Unity format RGB using [0, 1.0]
    /// </summary>
    /// <param name="h"></param>
    /// <returns></returns>
    public static Color GetRGBfromHSV(float _h, float _s, float _v)
    {
        return EditorGUIUtility.HSVToRGB(_h / 255.0f, _s / 255.0f, _v / 255.0f);
    }

    /// <summary>
    /// Converts HSV using [0, 255] to Unity format HSV using [0, 1.0]
    /// </summary>
    /// <param name="_h"></param>
    /// <param name="_s"></param>
    /// <param name="_v"></param>
    /// <returns>A vector containing HSV values</returns>
    public static Vector3 GetHSVUnitFromHSV255(float _h, float _s, float _v)
    {
        Vector3 unitHSV = new Vector3(_h / 255.0f, _s / 255.0f, _v / 255.0f);
        return unitHSV;
    }
}