using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask groundLayer; // Lapisan tanah
    public float moveSpeed = 5f; // Kecepatan gerakan pemain
    public float jumpForce = 5f; // Kekuatan lompatan
    private bool hasJumped = false; // Apakah pemain sudah melompat

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        // Mendapatkan input horizontal (A/D, Left Arrow/Right Arrow)
        float moveX = Input.GetAxis("Horizontal");

        // Membuat vektor gerakan dengan kecepatan
        Vector2 movement = new Vector2(moveX, 0f) * moveSpeed * Time.deltaTime;

        // Menggerakkan pemain
        transform.Translate(movement);
    }

    void Jump()
    {
        // Mengecek apakah pemain menekan tombol lompat dan apakah berada di tanah
        if (Input.GetButtonDown("Jump") && !hasJumped && IsGrounded())
        {
            // Menambahkan kekuatan lompat ke arah vertikal
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            hasJumped = true; // Pemain sudah melompat
        }
    }

    // Mengecek apakah pemain berada di tanah
    bool IsGrounded()
    {
        // Menentukan area kecil di bawah pemain untuk mendeteksi tanah
        Vector2 position = transform.position;
        float distance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, distance, groundLayer);

        return hit.collider != null; // Kembalikan true jika ada tabrakan dengan lapisan tanah
    }

    // Dijalankan ketika pemain menyentuh tanah
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsGrounded())
        {
            hasJumped = false; // Reset status lompatan jika pemain menyentuh tanah
        }
    }

    
}
