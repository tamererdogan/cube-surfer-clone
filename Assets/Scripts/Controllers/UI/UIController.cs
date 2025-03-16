using System;
using System.Collections.Generic;
using Enums;
using Signals.Core;
using UnityEngine;
using Zenject;

namespace Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;
        [Inject]
        private DiContainer _container;

        private readonly List<Transform> _layers = new();

        private void Awake()
        {
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
                _layers.Add(transform.GetChild(i));
        }

        private void Start()
        {
            OpenPanel(UIPanel.ProgressPanel, 0);
            OpenPanel(UIPanel.PlayPanel, 2);
        }

        private void OpenPanel(UIPanel uiPanel, byte layerIndex)
        {
            ClosePanel(layerIndex);
            _container.InstantiatePrefab(Resources.Load<GameObject>($"Prefabs/UI/{uiPanel}"), _layers[layerIndex]);
        }

        private void ClosePanel(byte layerIndex)
        {
            var childCount = _layers[layerIndex].childCount;
            for (var i = 0; i < childCount; i++)
                Destroy(_layers[layerIndex].GetChild(i).gameObject);
        }

        private void ShowStateSpecificUIPanel(GameStateChangedSignal signal)
        {
            switch (signal.State)
            {
                case GameState.Running:
                    ClosePanel(2);
                    break;
                case GameState.Failed:
                    OpenPanel(UIPanel.ReplayPanel, 2);
                    break;
                case GameState.Finished:
                    OpenPanel(UIPanel.NextLevelPanel, 2);
                    break;
                case GameState.PreGame:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(ShowStateSpecificUIPanel);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(ShowStateSpecificUIPanel);
        }
    }
}
