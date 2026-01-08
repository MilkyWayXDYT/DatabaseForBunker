using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JsonStruct : MonoBehaviour
{
    public Dictionary<string, List<Dictionary<string, object>>> table = new Dictionary<string, List<Dictionary<string, object>>>();
}
