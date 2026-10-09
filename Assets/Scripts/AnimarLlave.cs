using UnityEngine;

public class AnimarLlave : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    [SerializeField] private Vector3 velocidadRotacion = new Vector3(0f, 90f, 0f);

    [Header("Configuración de Flotado")]
    [SerializeField] private bool flotar = true;
    [SerializeField] private float amplitudFlotado = 0.2f; // Altura del movimiento arriba/abajo
    [SerializeField] private float velocidadFlotado = 2f;    // Velocidad del movimiento

    private Vector3 posicionInicial;

    private void Start()
    {
        // Guarda la posición original en la que apareció la llave
        posicionInicial = transform.position;
    }

    private void Update()
    {
        // Giro continuo
        transform.Rotate(velocidadRotacion * Time.deltaTime);

        // Movimiento suave de flotado (arriba y abajo)
        if (flotar)
        {
            float nuevoY = posicionInicial.y + (Mathf.Sin(Time.time * velocidadFlotado) * amplitudFlotado);
            transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
        }
    }
}