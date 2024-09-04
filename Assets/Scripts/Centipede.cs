using UnityEngine;
using System.Collections.Generic;

public class Centipede : MonoBehaviour
{
    private List<CentipedeSegments> segments = new List<CentipedeSegments>();
    public CentipedeSegments segmentPrefab;
    public Sprite headSprite;
    public Sprite bodySprite;

    public int size = 12;
    public float speed = 20f;
    public LayerMask collisionMask;
    public BoxCollider2D homeArea;
    private void Start()
    {
        Respawn();
    }
    private void Respawn()
    {
        foreach( CentipedeSegments segment in segments ) 
        { 
            Destroy(segment.gameObject);
        }
        segments.Clear();

        for ( int i = 0; i < size; i++ )
        {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            CentipedeSegments segment = Instantiate(segmentPrefab, position, Quaternion.identity);
            segment.spriteRenderer.sprite = i == 0 ? headSprite : bodySprite;
            segment.centipede = this;
            segments.Add(segment);
        }
        for ( int i = 0; i < segments.Count; i++ )
        {
            CentipedeSegments segment = segments[i];
            segment.ahead = GetSegmentAt(i - 1);
            segment.behind = GetSegmentAt(i + 1);
        }
    }
    private CentipedeSegments GetSegmentAt ( int index)
    {
        if (index >= 0 && index < segments.Count)
        {
            return segments[index];
        }
        else
        {
            return null;
        }
    }
    private Vector2 GridPosition( Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
}
