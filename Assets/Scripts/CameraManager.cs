using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    private Dictionary<int, Transform> cameraTransitions = new();
    [SerializeField] private List<Transform> cameraPositions;

    private void Awake() {
        for (int i = 0; i < 7; i++)
            cameraTransitions.Add(i + 1, cameraPositions[i]);

        cinemachineCamera.Follow = cameraPositions[0];
    }


    public void MoveToLevel(int level) {
        cinemachineCamera.Follow = cameraPositions[level];
    }
}
