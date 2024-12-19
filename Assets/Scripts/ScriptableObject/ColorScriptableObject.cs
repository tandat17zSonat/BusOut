using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorScriptableObject", menuName = "ColorScriptableObject")]
public class ColorScriptableObject : ScriptableObject
{
    [SerializeField] List<Sprite> sprites;

    // map để lấy sprite nhanh hơn theo name
    private Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    public Sprite GetSprite(CarColor color)
    {
        string name = color.ToString() + "_color";
        if (this.spritesDict.TryGetValue(name, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            foreach (Sprite sp in this.sprites)
            {
                if (sp.name.ToLower() == name.ToLower())
                {
                    this.spritesDict[name] = sp;
                    return sp;
                }
            }
        }
        return null;
    }
}
