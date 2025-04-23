using UnityEngine;

public class Monstruo : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float stopDistance = 1.5f;

    [SerializeField] GameObject gameOver;

    private void Update()
    {
        if (player == null) return;

        // Distancia al jugador
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            // Mover hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Girar hacia el jugador
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(other);
        }
    }
}
