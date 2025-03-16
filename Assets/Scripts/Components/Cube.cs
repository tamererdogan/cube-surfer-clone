using Abstracts;
using Enums;
using UnityEngine;

namespace Components
{
    public class Cube : MonoBehaviour, ITypeAssignable<CubeType>
    {
        [SerializeField]
        private CubeType cubeType;

        public CubeType GetObjectType()
        {
            return cubeType;
        }
    }
}