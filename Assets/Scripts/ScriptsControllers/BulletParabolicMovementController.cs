using System.Collections;
using UnityEngine;

namespace ScriptsControllers
{
    public class BulletParabolicMovementController : MonoBehaviour
    {
        [SerializeField] private float timeForDestroy = 5f;
    
        private void OnEnable()
        {
            StartCoroutine(DestroyBullet());
        }
        
        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(timeForDestroy);
            gameObject.SetActive(false);
        }
    }
}
