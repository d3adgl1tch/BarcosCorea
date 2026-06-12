using Unity.Cinemachine;
using UnityEngine;

public class MainCameraTrigger : MonoBehaviour
{
    public CinemachineCamera mainCamera;
    public CinemachineCamera mediumCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        mainCamera.Priority = 30;
        mediumCamera.Priority = 10;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        mainCamera.Priority = 0;
        mediumCamera.Priority = 30;
    }
}