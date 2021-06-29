using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float bulletSpeed = 40f;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource shotAudioSource;
    public AudioClip shotAudioClip;

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;
        shotAudioSource.PlayOneShot(shotAudioClip);
        Destroy(spawnedBullet, 2);
    }
}