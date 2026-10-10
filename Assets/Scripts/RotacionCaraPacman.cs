using UnityEngine;

public class RotacionCaraPacman : MonoBehaviour
{
    private Rigidbody rbPadre;
    public float velocidadRotacion = 15f;

    void Start()
    {
        // Busca el Rigidbody en el objeto padre (Jugador)
        rbPadre = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if (rbPadre != null)
        {
            // Calculamos la dirección del movimiento horizontal (ejes X y Z)
            Vector3 direccionMovimiento = new Vector3(rbPadre.linearVelocity.x, 0, rbPadre.linearVelocity.z);

            // Si el jugador se está moviendo (se presiona una tecla de dirección)
            if (direccionMovimiento.magnitude > 0.1f)
            {
                // Calculamos la rotación hacia esa dirección de movimiento
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);
                
                // Rotamos suavemente todo el cuerpo hacia esa dirección
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
            }
        }
    }
}