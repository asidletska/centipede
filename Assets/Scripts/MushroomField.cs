using UnityEngine;
using System.Collections.Generic;

public class MushroomField : MonoBehaviour
{
    private List<Mushroom> mushrooms;
    private BoxCollider2D area;
    public Mushroom prefab;
    public int amount = 50;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
        mushrooms = new List<Mushroom>();
    }
    public void Generate()
    {
        Bounds bounds = area.bounds;

        for (int i = 0; i < amount; i++)
        {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

           Mushroom mushroom = Instantiate(prefab, position, Quaternion.identity, transform);
            mushrooms.Add(mushroom);
        }
    }
    public void Clear()
    {
        foreach (Mushroom m in mushrooms)
        {
            Destroy(m.gameObject);
        }
        mushrooms.Clear();
    }
}
