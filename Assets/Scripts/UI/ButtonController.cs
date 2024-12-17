using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] TMP_InputField m_InputField;
    [SerializeField] ButtonHandler buttonHandler;

    public void Click()
    {
        int num;
        int.TryParse(m_InputField.text, out num);

        buttonHandler.HandleClick(num);
    }
}
