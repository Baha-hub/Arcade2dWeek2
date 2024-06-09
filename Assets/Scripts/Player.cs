using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SignalRClient Instance;
    public string username;
    public bool isMine;
    public float moveSpeed = 5f;
    private Vector3 lastPosition;
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;

    private void Start()
    {
        Instance = SignalRClient.Instance;
        StartCoroutine(SendPositionRoutine());
    }

    private IEnumerator SendPositionRoutine()
    {
        while (true)
        {
            if (isMine)
            {
                SendPositionToServer();
            }
            yield return new WaitForSeconds(0.1f); // Her 0.1 saniyede bir pozisyon gönder
        }
    }

    private void SendPositionToServer()
    {
        if (transform.position != lastPosition)
        {
            Instance.SendMessageToServer("SET_POSITION " + Instance.username+ " " + transform.position.x);
            lastPosition = transform.position;
        }
    }

    void Update()
    {
        if (isMine)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            // Uçağın mevcut pozisyonunu al
            Vector3 currentPosition = transform.position;

            // Yatay eksende hareket miktarını hesapla
            Vector3 movement = new Vector3(moveHorizontal, 0, 0);

            // Yeni pozisyonu hesapla
            Vector3 newPosition = currentPosition + movement * moveSpeed * Time.deltaTime;

            // Uçağın pozisyonunu güncelle
            transform.position = newPosition;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        

    }

    private void Shoot()
    {
        GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 20f; // Merminin hızını ayarla
        Instance.SendMessageToServer("SHOOT " + username + " " + missileSpawnPoint.position.x + " " + missileSpawnPoint.position.y);
    }
    public void OnShoot(string username, Vector2 position)
    {
        if (!isMine)
        {
            GameObject missile = Instantiate(missilePrefab, position, Quaternion.identity);
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.up * 10f; // Merminin hızını ayarla
        }
    }
}
