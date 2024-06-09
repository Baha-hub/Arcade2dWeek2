using UnityEngine;

public class Missile : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f); // 5 saniye sonra yok et
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Çarpışma olduğunda yok et
    }
}