using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory Profile", menuName = "SO/FactorySO", order = 1)]
public class FactoryProfile : ScriptableObject
{
    public string Name;
    public string Location;
    public List<string> Products;
    public int OpenDate;
    public int Employees;
    public int SquareMeters;
    public GameObject Model;
}