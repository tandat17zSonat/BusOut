using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCarScriptableObject", menuName = "CarScriptableObject")]
public class CarScriptableObject : ScriptableObject
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<PolygonCollider2D> colliders;

    // map để lấy sprite nhanh hơn theo name
    private Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    // Biến lưu xe vừa chọn lựa chọn
    private CarController selectedCar;
    public CarController SelectedCar { get => selectedCar; set => selectedCar = value; }

    // Số lượng id của direction 
    private int NUM_DIRECTION_ID = 6;

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
        int idx = (car.GetDirectionId() - 1) + car.GetSizeId() * NUM_DIRECTION_ID;
        return this.colliders[idx].points;
    }
}
