using UnityEngine;
using UnityEngine.UI;

public class BoudaryController : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        var controller = collision.GetComponent<CarDataController>();
        controller.IsStay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var controller = collision.GetComponent<CarDataController>();
        controller.IsStay = false;
    }

    public void ShowBoundary(Toggle toggle)
    {
        gameObject.SetActive(toggle.isOn);
    }
}
