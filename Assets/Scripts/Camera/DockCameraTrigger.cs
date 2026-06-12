using Unity.Cinemachine;
using UnityEngine;

public class DockCameraTrigger : MonoBehaviour
{
    public CinemachineCamera dockCamera;
    public CinemachineCamera mediumCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        dockCamera.Priority = 30;
        mediumCamera.Priority = 10;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        dockCamera.Priority = 0;
        mediumCamera.Priority = 30;
    }
}