using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupController : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    private string selectedToggleName;

    public string SelectedToggleName { get; private set; }
    void Start()
    {
        // Gắn sự kiện onValueChanged cho từng toggle
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener((isOn) => OnToggleChanged(toggle, isOn));
        }
    }

    private void OnToggleChanged(Toggle toggle, bool isOn)
    {
        if (isOn) // Nếu toggle được bật
        {
            SelectedToggleName = toggle.name;
        }
    }
}
