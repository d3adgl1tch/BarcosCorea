using UnityEngine;

public class EfectoAguaUI : MonoBehaviour
{
    [Header("Subir/Bajar")]
    public bool usarMovimientoVertical = true;
    public float intensidadVertical = 15f; // Qué tanto sube y baja (en píxeles)
    public float velocidadVertical = 2f;    // Qué tan rápido lo hace

    [Header("Izquierda/Derecha")]
    public bool usarMovimientoLateral = true;
    public float intensidadLateral = 10f;   // Qué tanto se mueve a los lados
    public float velocidadLateral = 1.5f;   // Qué tan rápido (usa un valor algo diferente al vertical para que no sea un círculo perfecto)

    private RectTransform rectTransform;
    private Vector2 posicionInicial;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("Colocar un RectTransform. Colócalo en un objeto de UI.", gameObject);
            enabled = false;
            return;
        }

        // Guardamos la posición original para que el vaivén sea relativo a su lugar de diseño
        posicionInicial = rectTransform.anchoredPosition;
    }

    void Update()
    {
        Vector2 nuevaPosicion = posicionInicial;

        // Calculamos el desfase vertical usando Mathf.Sin
        if (usarMovimientoVertical)
        {
            float desfasarY = Mathf.Sin(Time.time * velocidadVertical) * intensidadVertical;
            nuevaPosicion.y += desfasarY;
        }

        // Calculamos el desfase lateral
        if (usarMovimientoLateral)
        {
            // Sumamos un pequeño offset (como +1f) dentro del Sin si quieres que los ciclos no inicien exactamente iguales
            float desfasarX = Mathf.Sin(Time.time * velocidadLateral) * intensidadLateral;
            nuevaPosicion.x += desfasarX;
        }

        // Aplicamos la posición calculada
        rectTransform.anchoredPosition = nuevaPosicion;
    }
}
