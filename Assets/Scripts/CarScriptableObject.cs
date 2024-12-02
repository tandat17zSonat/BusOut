using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCarScriptableObject", menuName = "CarScriptableObject")]
public class CarScriptableObject : ScriptableObject
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<PolygonCollider2D> colliders;
    private Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    private CarController selectedCar;
    public CarController SelectedCar { get => selectedCar; set => selectedCar = value; }

    

    public Sprite GetSprite(CarData car)
    {
        string name = car.GetSpriteName();
        if (this.spritesDict.TryGetValue(name, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            foreach(Sprite sp in this.sprites)
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

    public Vector2[] GetCollisionPoints(CarData car)
    {
        int idx = (car.GetDirectionId() - 1) + car.GetSizeId() * 5;
        return this.colliders[idx].points;
    }
}
