using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByComposition : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] public Ease _curveEase = Ease.Linear;
        public float Duration { get; set; }
        public float Strength { get; set; }
        public int CountOfLoop { get; set; }
        internal AnimationButtonType AnimationButtonType { get => _animationButtonType; set => _animationButtonType = value; }

        [ContextMenu("Play animation")]
        private void PlayAnimation()
        {
            ActivateAnimation();
        }
        [ContextMenu("Stop animation")]
        private void StopAnimation()
        {
            _rectTransform.DOKill();
        }

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _rectTransform ??= GetComponent<RectTransform>();
        }


        private void OnButtonClick() =>
            ActivateAnimation();

        private void ActivateAnimation()
        {
            switch (AnimationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _rectTransform.DOShakeRotation(Duration, Vector3.forward * Strength).SetEase(_curveEase);
                    break;

                case AnimationButtonType.ChangePosition:
                    _rectTransform.DOShakeAnchorPos(Duration, Vector2.one * Strength).SetEase(_curveEase);
                    break;
                case AnimationButtonType.ChangeSize:
                    _rectTransform.DOShakeScale(Duration, Vector2.one * Strength).SetEase(_curveEase).SetLoops(CountOfLoop);
                    break;
            }
        }
    }
}
