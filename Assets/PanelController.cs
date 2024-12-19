using TMPro;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;
    public void SetResult(bool win)
    {
        if(win)
        {
            m_Text.text = "YOU WIN";
        }
        else
        {
            m_Text.text = "Yoy Loss";
        }
        ShowPanel(true);

    }
    public void ShowPanel(bool show)
    {
        gameObject.SetActive(show);
    }
}
