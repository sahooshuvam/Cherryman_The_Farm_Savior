using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherrySpawner : MonoBehaviour
{
    [SerializeField] private GameObject cherryPrefab;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform raycastDestination;
    [SerializeField] private bool attackAnimation;

    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float yRotate = 0.25f;
    private float nextFire;//When Next Fire Will Happend


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
            nextFire = Time.time + fireRate;// After Some Time next fire will Happen
            ray.origin = raycastOrigin.position; // Raycast Origin position
            ray.direction = raycastDestination.position - raycastOrigin.position;// Where Raycast will hit

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != this.gameObject) // If raycast will hit any object other then cherryman
                {
                    AttackAnimation();
                    GameObject cherryTemp = Instantiate(cherryPrefab, raycastOrigin.position, raycastOrigin.rotation);// Instantiate cherry prefab
                    cherryTemp.GetComponent<Cherry>().Fire(hit.point, raycastOrigin.position, transform.forward, 10f); // Now It will be fire in that POsition
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
