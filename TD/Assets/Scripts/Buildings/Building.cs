using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2Int _gridPosiotion;
    [SerializeField] private int _costOrganic;
    [SerializeField] private int _costMetal;
}
