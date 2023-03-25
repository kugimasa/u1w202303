using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/RhymeDataSet")]
    public class RhymeDataSet : ScriptableObject
    {
        [SerializeField] private RhymeData[] rhymeDataArray = new RhymeData[StaticConst.INPUT_NUM];
        public RhymeData[] RhymeDataArray => rhymeDataArray;
    }
}