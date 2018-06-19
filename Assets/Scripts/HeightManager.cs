using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightManager : MonoBehaviour
{
    [SerializeField]
    Terrain tera;

    [SerializeField]
    AudioSource audioSource;

    int hmWidth, hmHeight;
    float[,] heights;
    float[,] heightsTemp;

    float[] sample = new float[128];

    float timeSinceLastUpdate;

    [SerializeField]
    float updateRate = 0.02f;

    void Start()
    {
        tera = Terrain.activeTerrain;
        hmWidth = tera.terrainData.heightmapWidth;
        hmHeight = tera.terrainData.heightmapHeight;
        heights = tera.terrainData.GetHeights(0, 0, hmWidth, hmHeight);
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        if(timeSinceLastUpdate > updateRate)
        {
            UpdateHeights();
            timeSinceLastUpdate = 0;
        }
    }

    void UpdateHeights()
    {
        for (int i = hmHeight - 1; i > 0; i--)
        {
            for (int j = hmWidth - 1; j > 0; j--)
            {
                heights[i, j] = heights[i, j - 1];
            }
        }

        audioSource.GetSpectrumData(sample, 0, FFTWindow.Blackman);
        for (int i = 0; i < Mathf.Floor(hmHeight/2) - 1; i+=2)
        {
            float val = sample[i];
            //val = Mathf.Log(2 * val - val + 1, 2);
            heights[i, 0] = val;
            heights[i + 1, 0] = val;
        }

        tera.terrainData.SetHeights(0, 0, heights);
    }
}
