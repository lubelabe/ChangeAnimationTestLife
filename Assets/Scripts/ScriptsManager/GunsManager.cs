using System;
using UnityEngine;

namespace ScriptsManager
{
    public class GunsManager : MonoBehaviour
    {
        protected Action<GameObject> OnShoot;
        [NonSerialized] public Vector3 _positionInitial;
        [NonSerialized] public Quaternion _rotationInitial;
        [SerializeField] protected Transform _spawnToBullet;

        [SerializeField] protected SOSettingsWeapons _settingsGun;
        [SerializeField] private Transform _containerBullets;

        private void Awake()
        {
            _positionInitial = transform.position;
            _rotationInitial = transform.rotation;
        }
        
        public void ShootNow()
        {
            var bullet = GetBullet();
            OnShoot?.Invoke(bullet);
        }

        private GameObject GetBullet()
        {
            foreach (Transform bullet in _containerBullets.transform)
            {
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bullet.transform.position = _spawnToBullet.position;
                    bullet.gameObject.SetActive(true);
                    return bullet.gameObject;
                }
            }
            
            return null;
        }
    }
}