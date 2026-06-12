using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]

public class FishManager : MonoBehaviour
{
    [SerializeField] private GameObject FishPrefab;
    [SerializeField] private GameObject FishRef;
    [SerializeField] private int fishToSpawn;
    [SerializeField] private List<GameObject> fishes;
    private BoxCollider box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        box = GetComponent<BoxCollider>();
    }
    public void SpawnFish()
    {
        //if(FishRef != null) Destroy(FishRef);
        GetRandomPoint();
        FishRef=Instantiate(FishPrefab, GetRandomPoint(), Quaternion.identity);
        fishes.Add(FishRef);
    }
    Vector3 GetRandomPoint()
    {
        Vector3 center = box.center + transform.position;
        Vector3 size = gameObject.transform.localScale;
        return center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
    }
    public void DestroyAllFishes()
    {
        foreach (GameObject fish in fishes){
            if (fish != null)
                Destroy(fish);
        }
        fishes.Clear();
    }
}
