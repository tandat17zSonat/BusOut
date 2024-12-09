using TMPro;
using UnityEngine;

public class GenerateCarHandler : MonoBehaviour
{
    [SerializeField] CarSpawner carSpawner;

    [SerializeField] TMP_InputField inputCar4;
    [SerializeField] TMP_InputField inputCar6;
    [SerializeField] TMP_InputField inputCar10;

    public void Generate()
    {
        int numCar4, numCar6, numCar10;
        int.TryParse(inputCar4.text, out numCar4);
        int.TryParse(inputCar6.text, out numCar6);
        int.TryParse(inputCar10.text, out numCar10);

        carSpawner.GenerateCar(numCar4, numCar6, numCar10);
    }


}
