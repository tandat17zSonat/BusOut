using System.Threading.Tasks;
using UnityEngine;

public class UICarController : MonoBehaviour
{
    [SerializeField] ToggleGroupController toggleGroupColor;
    [SerializeField] ToggleGroupController toggleGroupSize;
    [SerializeField] ToggleGroupController toggleGroupDirection;

    public async void updateCar()
    {
        // Delay để toggle thay đổi xong giá trị mới updateCar
        await Task.Delay(100);

        var color = toggleGroupColor.GetSelectedToggle<CarColor>();
        var size = toggleGroupSize.GetSelectedToggle<CarSize>();
        var direction = toggleGroupDirection.GetSelectedToggle<CarDirection>();
        Singleton<ToolManager>.Instance.UpdateCar(color, size, direction);
    }


}
