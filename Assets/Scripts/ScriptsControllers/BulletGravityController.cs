using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletGravityController : MonoBehaviour
{
    [SerializeField] private float effectRange;
    [SerializeField] private float forceGravity = 50;
    [SerializeField] private float timeForDestroy = 5f;
    [SerializeField] private LayerMask layerObjAtraction;

    private Collider[] objsNearby;
    GameObject objTemp = null;

    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }

    private void FixedUpdate()
    {
        objsNearby = Physics.OverlapSphere(transform.position, effectRange, layerObjAtraction);
        
        if (objsNearby.Length > 0)
        {
            foreach (var obj in objsNearby)
            {
                objTemp = obj.transform.gameObject;
                objTemp.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * forceGravity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, effectRange);
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeForDestroy);
        gameObject.SetActive(false);
    }
}
