using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }
    [SerializeField] List<GameObject> groundPrefabs;
    [SerializeField] List<GameObject> sidePrefabs;
    Queue<GameObject> groundPool = new Queue<GameObject>();
    Queue<GameObject> sidePool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    public void GrowPool()
    {
        int size = 5;
        for (int i = 0; i < size; i++)
        {
            GameObject ground = Instantiate(groundPrefabs[Random.Range(0, groundPrefabs.Count)]);
            ground.transform.SetParent(transform);
            AddToGroundPool(ground);
            
            GameObject left = Instantiate(sidePrefabs[Random.Range(0,sidePrefabs.Count)]);
            left.transform.SetParent(transform);
            AddToSidePool(left);

            GameObject right = Instantiate(sidePrefabs[Random.Range(0, sidePrefabs.Count)]);
            right.transform.SetParent(transform);
            AddToSidePool(right);
        }
    }

    public void AddToGroundPool(GameObject obj)
    {
        obj.SetActive(false);
        groundPool.Enqueue(obj);
    }

    public void AddToSidePool(GameObject obj)
    {
        obj.SetActive(false);
        sidePool.Enqueue(obj);
    }

    public GameObject GetFromGroundPool()
    {
        if (groundPool.Count == 0)
            GrowPool();

        GameObject groundObj = groundPool.Dequeue();
        groundObj.SetActive(true);
        return groundObj;
    }

    public GameObject GetFromSidePool()
    {
        if (sidePool.Count == 0)
            GrowPool();

        GameObject sideObj = sidePool.Dequeue();
        sideObj.SetActive(true);
        return sideObj;
    }
}
