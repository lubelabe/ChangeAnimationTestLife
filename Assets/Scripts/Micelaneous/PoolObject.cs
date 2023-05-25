using System;
using UnityEngine;

namespace Micelaneous
{
    [Serializable]
    public class PoolObject
    {
        public string IdPool;
        public GameObject ObjectForPool;
        public Transform ContainerObjectPool;
        public int SizePool;
    }
}
