using ScriptsManager;
using UnityEngine;

namespace ScriptsControllers
{
    public class GunParabolicMovementControll : GunsManager
    {
        private void OnEnable()
        {
            OnShoot += Shoot;
        }
        
        private void Shoot(GameObject currentBullet)
        {
            var riggidBody = currentBullet.GetComponent<Rigidbody>();
            var angleBullet = _settingsGun.powerForAngle * Vector3.up;
            riggidBody.velocity = (_spawnToBullet.forward * _settingsGun.powerOfShoot) + angleBullet;
        }

        private void OnDisable()
        {
            OnShoot -= Shoot;
        }
    }
}
