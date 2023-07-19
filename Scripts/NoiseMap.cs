using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMap
{
    public static Texture2D GenerateNoiseMap(int width, int height, float scale)
    {
        Texture2D noiseTextureMap = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise((float) x / width * scale, (float) y / height * scale);
                noiseTextureMap.SetPixel(x, y, new Color(0, noiseValue, 0));
            }
        }

        noiseTextureMap.Apply();
        return noiseTextureMap;
    }
}
