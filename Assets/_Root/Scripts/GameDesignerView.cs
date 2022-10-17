using Profile;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = nameof(GameDesignerView), menuName = nameof(GameDesignerView))]
    internal class GameDesignerView : ScriptableObject
    {
        [Header("Initial Settings")]
        [SerializeField] private float _speedCar;
        [SerializeField] private float _jumpCar;
        [SerializeField] private GameState _initialState;

        public float SpeedCar { get => _speedCar; set => _speedCar = value; }
        public float JumpCar { get => _jumpCar; set => _jumpCar = value; }
        internal GameState InitialState { get => _initialState; set => _initialState = value; }
    }
}