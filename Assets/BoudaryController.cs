using UnityEngine;
using UnityEngine.UI;

public class BoudaryController : MonoBehaviour
{
    [SerializeField] GameObject region;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var controller = collision.GetComponent<CarController>();
        controller.IsStay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var controller = collision.GetComponent<CarController>();
        controller.IsStay = false;
    }

    public void ShowBoundary(Toggle toggle)
    {
        region.SetActive(toggle.isOn);
    }
}
