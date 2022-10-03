using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;
    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
