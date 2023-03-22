using UnityEngine;

namespace Script.Model
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/RhymeData")]
    public class RhymeData : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private string _text;
        [SerializeField] private RhymeType _type;
    }
}