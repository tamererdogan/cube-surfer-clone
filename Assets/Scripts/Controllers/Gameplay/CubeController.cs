using System.Collections;
using Components;
using Enums;
using Signals.Cube;
using Signals.Player;
using Signals.Score;
using UnityEngine;
using Zenject;

namespace Controllers.Gameplay
{
    public class CubeController : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        private void OnTriggerEnter(Collider other)
        {
            if (transform.parent == null) return;
            other.TryGetComponent<Cube>(out var otherCube);
            if (otherCube == null) return;
            switch (otherCube.GetObjectType())
            {
                case CubeType.Wall:
                    transform.SetParent(null);
                    _signalBus.Fire(new CubeRemovedSignal());
                    StartCoroutine(RemoveObjectAfterSeconds(2));
                    break;
                case CubeType.Obstacle:
                    _signalBus.Fire(new CubeRemovedSignal());
                    StartCoroutine(RemoveObjectAfterSeconds(1));
                    var colliders = gameObject.GetComponents<Collider>();
                    foreach (var col in colliders)
                        col.enabled = false;
                    other.enabled = false;
                    break;
                case CubeType.Diamond:
                    other.gameObject.SetActive(false);
                    _signalBus.Fire(new ScoreCollectedSignal());
                    break;
                case CubeType.Finish:
                    _signalBus.Fire(new PlayerFinishedSignal());
                    break;
                case CubeType.Default:
                default:
                    break;
            }
        }

        private IEnumerator RemoveObjectAfterSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            transform.SetParent(null);
            transform.gameObject.SetActive(false);
        }
    }
}
