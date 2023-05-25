using System;
using System.Collections.Generic;
using UnityEngine;

namespace Micelaneous
{
    public class PoolObjectsManager : MonoBehaviour
    {
        [SerializeField] private List<PoolObject> _pools = new List<PoolObject>();

        private void Start()
        {
            CreatePoolObjects();
        }

        private void CreatePoolObjects()
        {
            foreach (var pool in _pools)
            {
                for (var j = 0; j < pool.SizePool; j++)
                {
                    var objectTemp = Instantiate(pool.ObjectForPool, Vector3.zero, Quaternion.identity, pool.ContainerObjectPool);
                    objectTemp.SetActive(false);
                }
            }
        }
    }
}
