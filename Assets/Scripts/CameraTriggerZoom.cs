using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraTriggerZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform player;
    [SerializeField] private DockScript dockScript;

    [SerializeField] private float fovIN = 40f;
    [SerializeField] private float durationOut = 1f;
    [SerializeField] private float durationIn = 1f;

    private float fovOUT;
    private Quaternion originRotation;
    private bool focusPlayer;

    private Tween fovTween;
    private Tween lookAtTween;

    private void Start()
    {
        fovOUT = cam.fieldOfView;
        originRotation = cameraTransform.rotation;
    }
   // Vector3 followPlayer = Vector3.Lerp()
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !dockScript.isActiveAndEnabled) return;

        focusPlayer = true;
        
        fovTween?.Kill();
        fovTween = cam.DOFieldOfView(fovIN, durationIn)
            .SetEase(Ease.InSine);

        //lookAtTween = cameraTransform.DOLookAt(player.position,durationIn).SetEase(Ease.InOutSine);
        //lookAtTween = cameraTransform.
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        focusPlayer = false;

        fovTween?.Kill();
        fovTween = cam.DOFieldOfView(fovOUT, durationOut)
            .SetEase(Ease.InSine);


        //cameraTransform.DORotateQuaternion(originRotation, durationOut)
        //    .SetEase(Ease.InSine);
    }
    private void Update()
    {
        //if (focusPlayer)
        {
            //cameraTransform.LookAt(player.position);
        }
    }
}