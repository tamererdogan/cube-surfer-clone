using UnityEngine;
using UnityEngine.UI;

namespace Abstracts
{
    public abstract class AbstractButtonEventController : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
        }

        protected void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();

        protected void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}