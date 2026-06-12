using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TimonMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerAngleMovement;
    void Update()
    {
        MoveTimon();
    }
    public void MoveTimon()
    {
        if (playerAngleMovement == null || !playerAngleMovement.isActiveAndEnabled) return;

        if (playerAngleMovement.GetIsPressed == true)
        {
            float angle = playerAngleMovement.GetAngle;
            float playerRotationSpeed = playerAngleMovement.GetRotationSpeed;
            //transform.localRotation = Quaternion.Euler(0, 0, -targetAngle);
            transform.Rotate(Vector3.forward * -playerAngleMovement.GetAngle * playerAngleMovement.GetRotationSpeed);
        }
    }
}
