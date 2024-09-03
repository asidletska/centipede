using UnityEngine;
using System.Collections.Generic;

public class Centipede : MonoBehaviour
{
    private List<CentipedeSegments> segments = new List<CentipedeSegments>();
    public CentipedeSegments segmentPrefab;
    public Sprite headSprite;
    public Sprite bodySprite;
    public int size = 12;
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
            segments.Add(segment);
        }
    }
    private Vector2 GridPosition( Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
}
