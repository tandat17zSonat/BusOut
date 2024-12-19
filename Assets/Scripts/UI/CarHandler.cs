using System.Threading.Tasks;
using UnityEngine;

public class CarManager : Singleton<CarManager>
{
    [SerializeField] ToggleGroupController toggleGroupColor;
    [SerializeField] ToggleGroupController toggleGroupSize;
    [SerializeField] ToggleGroupController toggleGroupDirection;

    public async void updateCar()
    {
        if( Singleton<GameplayManager>.Instance.State == GameState.TOOL)
        {
            // Delay để toggle thay đổi xong giá trị mới updateCar
            await Task.Delay(100);

            var color = toggleGroupColor.GetSelectedToggle<CarColor>();
            var size = toggleGroupSize.GetSelectedToggle<CarSize>();
            var direction = toggleGroupDirection.GetSelectedToggle<CarDirection>();
            Singleton<ToolManager>.Instance.UpdateCar(color, size, direction);
        }
        
    }

    public void ChangeToggleValue(CarColor color, CarSize size, CarDirection direction)
    {
        toggleGroupColor.SetSelectedToggle<CarColor>(color);
        toggleGroupSize.SetSelectedToggle<CarSize>(size);
        toggleGroupDirection.SetSelectedToggle<CarDirection>(direction);
    }
}
