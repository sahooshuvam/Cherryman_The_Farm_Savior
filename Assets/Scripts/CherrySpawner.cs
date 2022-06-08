using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherrySpawner : MonoBehaviour
{
    //[SerializeField] private ParticleSystem muzzleFlash;
    //[SerializeField] private ParticleSystem projectileImpect;
    //[SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject cherryPrefab;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform raycastDestination;
    [SerializeField] private bool attackAnimation;

    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float yRotate = 0.25f;
    private float nextFire;


    private new AnimationScript animation;

    RaycastHit hit;
    Ray ray;

    private void Awake()
    {
        animation = GetComponent<AnimationScript>();
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ray.origin = raycastOrigin.position;
            ray.direction = raycastDestination.position - raycastOrigin.position;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != this.gameObject)
                {
                    AttackAnimation();
                    GameObject projectileTemp = Instantiate(cherryPrefab, raycastOrigin.position, raycastOrigin.rotation);
                    projectileTemp.GetComponent<Cherry>().Fire(hit.point, raycastOrigin.position, transform.forward, 10f);
                }
            }
        }
    }

    private void AttackAnimation()
    {
        if (attackAnimation) 
            animation.Attack();
    }
}
