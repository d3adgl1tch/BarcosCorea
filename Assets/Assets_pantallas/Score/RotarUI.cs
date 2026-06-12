using UnityEngine;

public class RotarUI : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    [Tooltip("Velocidad en grados por segundo. + giran a la izquierda, - a la derecha.")]
    public float velocidadRotacion = 90f;

    private RectTransform rectTransform;

    void Start()
    {
        // Referencia al RectTransform del elemento de UI
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("Este script necesita estar en un objeto de UI con un RectTransform.", gameObject);
            enabled = false;
        }
    }

    void Update()
    {
        // Multiplicamos por Time.deltaTime para que el giro sea suave e independiente de los FPS
        rectTransform.Rotate(0f, 0f, velocidadRotacion * Time.deltaTime);
    }
}
