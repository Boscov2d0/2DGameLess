using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;

    private GameDesignerView _designerView;
    private MainController _mainController;


    private void Start()
    {
        _designerView = (GameDesignerView)Resources.Load("GameDesignerView");
        var profilePlayer = new ProfilePlayer(_designerView.SpeedCar, _designerView.JumpCar, _designerView.InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
