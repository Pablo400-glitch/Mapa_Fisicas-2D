using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    // Este m�todo se llamar� cuando ocurra una colisi�n f�sica
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " ha colisionado con " + collision.gameObject.name);
    }

    // Este m�todo se llamar� cuando se active un trigger
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(gameObject.name + " ha activado el trigger con " + collider.gameObject.name);
    }
}
