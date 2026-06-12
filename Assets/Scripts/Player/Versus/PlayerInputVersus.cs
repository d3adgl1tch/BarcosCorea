using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputVersus : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private float rotationSpeed;

    private int activeFingerId = -1;
    private Vector2 lastPos;
    private float angle;
    private bool isPressed;

    public float GetAngle => angle;
    public float GetRotationSpeed => rotationSpeed;
    public bool GetIsPressed => isPressed;

    void Update()
    {
        if (Touchscreen.current == null)
            return;

        angle = 0f;

        // Si ya tengo un dedo asignado
        if (activeFingerId != -1)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.touchId.ReadValue() != activeFingerId)
                    continue;

                if (!touch.press.isPressed)
                {
                    ReleaseFinger();
                    return;
                }

                Vector2 pos = touch.position.ReadValue();
                angle = pos.x - lastPos.x;
                lastPos = pos;
                isPressed = true;
                return;
            }

            ReleaseFinger();
            return;
        }

        // Buscar SOLO dedos que ACABAN de tocar
        foreach (var touch in Touchscreen.current.touches)
        {
            if (!touch.press.wasPressedThisFrame)
                continue;

            Vector2 pos = touch.position.ReadValue();

            if (IsTouchInMyArea(pos))
            {
                activeFingerId = touch.touchId.ReadValue();
                lastPos = pos;
                isPressed = true;
                return;
            }
        }

        isPressed = false;
    }

    void ReleaseFinger()
    {
        activeFingerId = -1;
        isPressed = false;
    }

    bool IsTouchInMyArea(Vector2 pos)
    {
        return playerIndex == 0
            ? pos.x < Screen.width * 0.5f
            : pos.x >= Screen.width * 0.5f;
    }
}