using ScriptsManager;
using UnityEngine;

namespace ScriptsControllers
{
    public class GunGravityControll : GunsManager
    {
        private void OnEnable()
        {
            OnShoot += Shoot;
        }
        
        private void Shoot(GameObject currentBullet)
        {
            var riggidBody = currentBullet.GetComponent<Rigidbody>();
            riggidBody.velocity = _spawnToBullet.forward * _settingsGun.powerOfShoot;
        }

        private void OnDisable()
        {
            OnShoot -= Shoot;
        }
    }
}
