using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bricksLine;

    private List<GameObject> bricksOnLevel = new List<GameObject>();

    [SerializeField]
    private int minLinesCount = 3;
    [SerializeField]
    private int maxLinesCount = 8;

    [SerializeField]
    private float linesInterval = 0.5f;
    [SerializeField]
    private float offSet = 0;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        int linesCount = Random.Range(minLinesCount, maxLinesCount);

        for (int i = 0; i < linesCount; i++)
        {
            GameObject ObjectToSpawn = Instantiate(bricksLine[Random.Range(0, bricksLine.Count - 1)]);

            ObjectToSpawn.transform.parent = gameObject.transform;
            ObjectToSpawn.name = "Line " + (i + 1).ToString();

            Vector3 position = Vector3.up * i * linesInterval;
            position.y -= offSet;

            ObjectToSpawn.transform.position = position;

            bricksOnLevel.Add(ObjectToSpawn);
        }
    }

    private void DestroyLevel()
    {
            for (int i = 0; i < bricksOnLevel.Count; i++)
            {
                Destroy(bricksOnLevel[i]);
            }

            bricksOnLevel.Clear();
    }

    public void RegenerateLevel()
    {
        DestroyLevel();
        GenerateLevel();
    }
}
