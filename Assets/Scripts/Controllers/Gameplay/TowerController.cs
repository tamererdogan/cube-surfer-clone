using Components;
using Enums;
using Signals.Cube;
using UnityEngine;
using Zenject;

namespace Controllers.Gameplay
{
    public class TowerController : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;
        [SerializeField]
        private int cubeCount;

        private const float VerticalOffset = 1f;

        private void Awake()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/GameObjects/Cube");
            for (var i = 0; i < cubeCount; i++)
                Instantiate(prefab, transform).transform.localPosition =  Vector3.up * i * VerticalOffset;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<Cube>(out var otherCube);
            if (otherCube.GetObjectType() != CubeType.Default) return;
            _signalBus.Fire(new AddCubeSignal(cubeCount));
            transform.gameObject.SetActive(false);
        }
    }
}