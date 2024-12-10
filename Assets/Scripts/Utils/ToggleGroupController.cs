using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupController : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Type enumType;

    private string selectedToggleName;
    void Start()
    {
        // Gắn sự kiện onValueChanged cho từng toggle
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            if( toggle.isOn) selectedToggleName = toggle.name;
            toggle.onValueChanged.AddListener((isOn) => OnToggleChanged(toggle, isOn));
        }
    }

    private void OnToggleChanged(Toggle toggle, bool isOn)
    {
        if (isOn) // Nếu toggle được bật
        {
            selectedToggleName = toggle.name;
        }
    }

    public T GetSelectedToggle<T>() where T : Enum
    {
        return (T)Enum.Parse(typeof(T), selectedToggleName);
    }

    public void SetSelectedToggle<T>(T value) where T : Enum
    {
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
            if(toggle.name == value.ToString())
            {
                toggle.isOn = true;
            }
        }
    }
}
