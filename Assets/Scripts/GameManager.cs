using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Initializers;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraConfig cameraConfigs;
    [SerializeField] private CanvasConfig canvasConfigs;
    [SerializeField] private CharacterConfig wizardConfigs;
    [SerializeField] private CharacterConfig skeletonWarriorConfigs;
    
    private void Awake()
    {
        var mainCamera = new CameraInitializer(cameraConfigs, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        var skeleton = new CharacterInitializer(skeletonWarriorConfigs, new Vector3(0f, -2, -56f), new Vector3(-90, 0, 0));
        var canvas = new CanvasInitializer(canvasConfigs);
    }
}