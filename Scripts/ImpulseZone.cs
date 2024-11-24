using UnityEngine;

public class ImpulseZone : MonoBehaviour
{
    public float force = 10f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            Debug.Log("Objeto impulsado hacia adelante.");
        }
    }
}
