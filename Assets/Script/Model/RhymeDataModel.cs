using System.Collections.Generic;
using Script.Data;
using UnityEngine;

namespace Script.Model
{
    public class RhymeDataModel : MonoBehaviour
    {
        [SerializeField] private List<RhymeDataSet> _battle1RhymeDataSets;
        [SerializeField] private List<RhymeDataSet> _battle2RhymeDataSets;
        [SerializeField] private List<RhymeDataSet> _battle3RhymeDataSets;

        public List<RhymeDataSet> GetCurrentRhymeDataSet(int id)
        {
            switch (id)
            {
                case 0:
                    return _battle1RhymeDataSets;
                case 1:
                    return _battle2RhymeDataSets;
                case 2:
                    return _battle3RhymeDataSets;
                default:
                    return _battle1RhymeDataSets;
            }
        }
    }
}