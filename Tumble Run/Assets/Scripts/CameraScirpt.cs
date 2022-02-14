using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScirpt : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 vec;

    void Start()
    {
        vec = transform.position - player.transform.position;
    }

    void Update()
    {
       transform.position = vec + player.transform.position;
    }
}
