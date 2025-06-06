using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemSO data;

    public int GetPOint()
    {
        return data.point;
    }
}
