using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPassengerScriptableObject", menuName = "PassengerScriptableObject")]
public class PassengerScriptableObject : ScriptableObject
{
    [SerializeField] List<Sprite> sprites;
    private Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    public Sprite GetSprite(PassengerData passenger)
    {
        string name = passenger.GetSpriteName();
        if (this.spritesDict.TryGetValue(name, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            foreach (Sprite sp in this.sprites)
            {
                if (sp.name == name)
                {
                    this.spritesDict[name] = sp;
                    return sp;
                }
            }
        }
        return null;
    }
}
