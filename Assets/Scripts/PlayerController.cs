using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed;

    private int counter;
    private int totalPickups;           // ← dodajemy nową zmienną
    public TMP_Text scoreText;
    public TMP_Text winText;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        counter = 0;

        // Automatyczne zliczenie wszystkich Pickupów w scenie:
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        totalPickups = pickups.Length;

        // Ukrywamy tekst zwycięstwa na start
        winText.gameObject.SetActive(false);

        // Wyświetlamy od razu początkowy stan (0/totalPickups)
        UpdateOnScreenScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            counter++;
            Destroy(collision.gameObject);
            UpdateOnScreenScore();
        }
    }

    void UpdateOnScreenScore()
    {
        // Wyświetlamy aktualny przebieg: counter z całkowitej liczby pickupów
        scoreText.text = $"Punkty: {counter}/{totalPickups}";

        if (counter >= totalPickups)
        {
            winText.gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2d.AddForce(movement * speed);
    }
}
