using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] float interval = 3f;
    float time;

  void Start() 
  {
    time = 0f;
  }    void Update() 
  {
    time += Time.deltaTime;
    while(time >= interval) 
    {
      PlankFires();
      time -= interval;
    }
  }
    void PlankFires()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);         Rigidbody rb = projectile.GetComponent<Rigidbody>();         rb.velocity = -transform.up * projectileSpeed;
    }
}
