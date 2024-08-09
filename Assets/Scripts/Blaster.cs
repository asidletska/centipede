using UnityEngine;

public class Blaster : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 direction;
    public float speed = 20f;

    private Camera mainCamera;
    private float screenLeftLimit;
    private float screenRightLimit;
    private float screenTopLimit;
    private float screenBottomLimit;
    private float playerWidth;
    private float playerHeight;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        mainCamera = Camera.main;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        playerWidth = renderer.bounds.size.x / 2;
        playerHeight = renderer.bounds.size.y / 2;
        screenLeftLimit = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + playerWidth;
        screenRightLimit = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - playerWidth;
        screenBottomLimit = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + playerHeight;
        screenTopLimit = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - playerHeight;
    }
    private void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        // ќбмеженн€ руху гравц€ в межах екрану
        float xClamp = Mathf.Clamp(transform.position.x, screenLeftLimit, screenRightLimit);
        float yClamp = Mathf.Clamp(transform.position.y, screenBottomLimit, screenTopLimit);
        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += direction.normalized * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);
    }
}
