using UnityEngine;

public class CentipedeSegments : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {  get; private set; }
    public Centipede centipede { get; set; }
    public CentipedeSegments ahead {  get; set; }
    public CentipedeSegments behind { get; set; }
    public bool isHead => ahead == null;


    private Vector2 direction = Vector2.right + Vector2.down;
    private Vector2 targetposition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetposition = transform.position;
    }

    private void Update()
    {
        if (isHead && Vector2.Distance(transform.position, targetposition) < 0.1f)
        {
            UpdateHeadSegment();
        }

        Vector2 currentPosition = transform.position;
        float speed = centipede.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetposition, speed);

        Vector2 movementDirection = (targetposition - currentPosition).normalized;
        float angle = Mathf.Atan2(movementDirection.x, movementDirection.y);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    private void UpdateHeadSegment()
    {
        Vector2 gridPosition = GridPosition(transform.position);

        targetposition = gridPosition;
        targetposition.x += direction.x;

        if (Physics2D.OverlapBox(targetposition, Vector2.zero, 0f, centipede.collisionMask))
        {
            direction.x = -direction.x;

            targetposition.x = gridPosition.x;
            targetposition.y = gridPosition.y + direction.y;

            Bounds homeBounds = centipede.homeArea.bounds;

            if ((direction.y == 1f && targetposition.y > homeBounds.max.y) || (direction.y == -1f && targetposition.y < homeBounds.min.y))
            {
                direction.y = -direction.y;
                targetposition.y = gridPosition.y + direction.y;
            }
        }

        if (behind != null)
        {
            behind.UpdateBodySegment();
        }
    }
    private void UpdateBodySegment()
    {
        targetposition = GridPosition(ahead.transform.position);
        direction = ahead.direction;

        if (behind != null)
        {
            behind.UpdateBodySegment();
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
}
