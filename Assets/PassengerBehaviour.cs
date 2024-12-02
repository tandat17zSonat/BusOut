using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerBehaviour : MonoBehaviour
{
    private PassengerData passengerData;
    // Start is called before the first frame update
    void Start()
    {
       this.passengerData = this.GetComponent<PassengerController>().PassengerData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(int posId)
    {
        int cellX = posId - 6 > 0 ? posId - 6 : 0;
        int cellY = posId - 6 > 0 ? 6 : posId;

        transform.position = new Vector2(- cellX * 0.6f, - cellY * 0.6f);
    }
}
