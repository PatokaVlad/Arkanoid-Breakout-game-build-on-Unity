using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> BonusObjects;

    private GameObject bonusObject;

    public void SpawnBonus(Vector3 position)
    {
        int bonusesCount = BonusObjects.Count;

        if (bonusesCount != 0)
        {
            bonusObject = BonusObjects[Random.Range(0, bonusesCount)];
            Instantiate(bonusObject, position, Quaternion.identity);
        }
    }
}
