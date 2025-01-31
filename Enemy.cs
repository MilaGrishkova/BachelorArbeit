﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioCollection audioCollect;
    public GameObject DestroyedEffect;
    bool isHit = false;
    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isHit)
        { 
            collision.gameObject.GetComponent<Player>().RecountHeart(-1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 2f, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            audioCollect.PlayKillSound();
            Instantiate(DestroyedEffect, transform.position,
            Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public IEnumerator Fatality()
    {
        isHit = true;
        GetComponent<AudioSource>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void dying()
    {
        StartCoroutine(Fatality());
    }
}