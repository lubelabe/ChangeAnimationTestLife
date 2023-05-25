using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BulletAtractionController : MonoBehaviour
{
    [SerializeField] private float _effectRange;
    [SerializeField] private float _attractionForce = 50;
    [SerializeField] private float _timeForDestroy = 5f;
    [SerializeField] private LayerMask _layerObjAtraction;

    private Collider[] _objsNearby;

    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }

    private void FixedUpdate()
    {
        _objsNearby = Physics.OverlapSphere(transform.position, _effectRange, _layerObjAtraction);
        foreach (var objectNearby in _objsNearby)
        {
            if (objectNearby.gameObject != gameObject)
            {
                var riggidbody = objectNearby.GetComponent<Rigidbody>();
                if (riggidbody != null)
                {
                    //Calculate direction and aplicate force of atraction
                    var direction = transform.position - objectNearby.transform.position;
                    riggidbody.AddForce(direction.normalized * _attractionForce);
                }
            }
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_timeForDestroy);
        gameObject.SetActive(false);
    }
}
