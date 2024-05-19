using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    public Transform player; // Referensi ke player
    public Vector2 parallaxEffectMultiplier = new Vector2(0.5f, 0.5f); // Mengatur efek parallax

    private Vector3 lastPlayerPosition;
    private float textureUnitSizeX;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Cari player jika belum diatur
        }
        lastPlayerPosition = player.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;

        // Debugging
        Debug.Log("Initial Player Position: " + lastPlayerPosition);
        Debug.Log("Texture Unit Size X: " + textureUnitSizeX);
    }

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned and no Player found with tag 'Player'");
            return;
        }

        Vector3 deltaMovement = player.position - lastPlayerPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastPlayerPosition = player.position;

        // Debugging
        Debug.Log("Player Position: " + player.position);
        Debug.Log("Background Position: " + transform.position);
        Debug.Log("Delta Movement: " + deltaMovement);

        if (Mathf.Abs(player.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (player.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(player.position.x + offsetPositionX, transform.position.y);

            // Debugging
            Debug.Log("Offset Position X: " + offsetPositionX);
            Debug.Log("Updated Background Position: " + transform.position);
        }
    }
}
