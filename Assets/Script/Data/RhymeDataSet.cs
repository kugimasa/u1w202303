using System.Collections.Generic;
using UnityEngine;

namespace Script.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/RhymeDataSet")]
    public class RhymeDataSet : ScriptableObject
    {
        [SerializeField] private List<RhymeData> _set;
        [SerializeField] private RhymeType _type;

        public List<RhymeData> Set => _set;
        public RhymeType Type => _type;
    }
}