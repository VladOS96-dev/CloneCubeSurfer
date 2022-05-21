using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level
{
    [CreateAssetMenu(fileName = "Levels")]
    public class ScriptableListSettings : ScriptableObject
    {
        public List<Level> Levels;
    }
}