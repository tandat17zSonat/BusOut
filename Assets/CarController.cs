using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] CarDataController carDataController;

    Vector2 oldPosition;
    Vector2 target;
    Vector2 veclocity = Vector2.zero;

    CarState _state = CarState.PARKING;




    public CarState State { get => _state; set => _state = value; }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var cData = Data as CarData;
        //switch (_state)
        //{
        //    case CarState.MOVE:
        //    case CarState.MOVE_TO_SLOT:
        //        {
        //            var v = (Vector3)veclocity * Config.VEC_CAR_MOVE;
        //            transform.position += v * Time.deltaTime;
        //            break;
        //        }
        //}

        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.y);
        //((CarData)data).Position = transform.localPosition; // Lưu lại vị trí xe mỗi khi xe di chuyển
    }
}
