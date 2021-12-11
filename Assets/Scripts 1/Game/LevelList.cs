using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelList
{
    public List<Level> easy = new List<Level>();
    public List<Level> hard = new List<Level>();
}

[System.Serializable]
public class Level
{
    public bool completed;
    public float goalAmount;
    public bool[] availableMoney = new bool[14];
    // [0.01, 0.02, 0.05, 0.1, 0.2, 0.5, 1, 2, 5, 10, 20, 50, 100, 200]
}
