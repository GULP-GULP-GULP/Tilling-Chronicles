using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TechData
{
    public string techID;
    public string techName;
    public string description;
    public int cost;
    public List<string> prerequisites;
    public bool isUnlocked;
}
