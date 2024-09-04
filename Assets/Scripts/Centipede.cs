using UnityEngine;
using System.Collections.Generic;

public class Centipede : MonoBehaviour
{
    private List<CentipedeSegments> segments = new List<CentipedeSegments>();
    public CentipedeSegments segmentPrefab;
    public Mushroom mushroomPrefab;
    public Sprite headSprite;
    public Sprite bodySprite;

    public int size = 12;
    public int pointsHead = 100;
    public int pointsBody = 20;
    public float speed = 20f;
    public LayerMask collisionMask;
    public BoxCollider2D homeArea;

    public void Respawn()
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
    public void Remove(CentipedeSegments segment)
    {
        GameManager.Instance.IncreaseScore(segment.isHead ? pointsBody : pointsHead);

        Vector3 position = GridPosition(segment.transform.position);
        Instantiate(mushroomPrefab, position, Quaternion.identity);

        if (segment.ahead != null)
        {
            segment.ahead.behind = null;
        }

        if (segment.behind != null)
        {
            segment.behind.ahead = null;
            segment.behind.spriteRenderer.sprite = headSprite;
            segment.behind.UpdateHeadSegment();
        }

        segments.Remove(segment);
        Destroy(segment.gameObject);

        if (segments.Count == 0)
        {
            GameManager.Instance.NextLevel();
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
