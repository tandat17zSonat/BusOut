using TMPro;
using UnityEngine;

public class IOHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField inputLevel;

    public void SaveToJson()
    {
        int level;
        int.TryParse(inputLevel.text, out level);

        Singleton<GameManager>.Instance.Save(level);
    }

    public void LoadFromJson()
    {
        int level;
        int.TryParse(inputLevel.text, out level);

        Singleton<GameManager>.Instance.Load(level);
    }
}
