using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> obs;
    private Queue<GameObject> sph;

    [SerializeField] private GameObject obstalcePrefab = null;
    [SerializeField] private GameObject spherePrefab = null;
    [SerializeField] private int obstalceSize;
    [SerializeField] private int sphereSize;

    [SerializeField] private float obsDistance;
    [SerializeField] private float sphDistance;

    private Vector3 obsVec;
    private Vector3 sphVec;

    private void Awake()
    {
        obs = new Queue<GameObject>();
        obsVec = new Vector3(0, 0, 15);
        for (int i = 0; i < obstalceSize; i++)
        {
            GameObject go = Instantiate(obstalcePrefab, obsVec, Quaternion.identity);
            obsVec.z += obsDistance;
            obs.Enqueue(go);
        }

        sph = new Queue<GameObject>();

        sphVec = new Vector3(Random.Range(-1.6f, 1.6f), 1.5f, 17);
        for (int i = 0; i < sphereSize; i++)
        {
            GameObject gb = Instantiate(spherePrefab, sphVec, Quaternion.identity);
            sphVec.x = Random.Range(-1.6f, 1.6f);
            sphVec.z += sphDistance;
            sph.Enqueue(gb);
        }

    }
}
