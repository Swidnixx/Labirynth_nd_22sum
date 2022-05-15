using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorMapping[] mappings;
    public float offset = 5;

    public void DestroyMap()
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        for(int i=children.Length-1; i > 0; i--)
        {
            DestroyImmediate(children[i].gameObject);
        }
    }

    public void GenerateMap()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int z = 0; z < map.height; z++)
            {
                CreateTile(x, z);
            }
        }
    }

    private void CreateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);

        foreach(ColorMapping m in mappings)
        {
            if(m.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, 0, z) * offset;
                Instantiate(m.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
