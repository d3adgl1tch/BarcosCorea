using System.Collections.Generic;
using UnityEngine;

public class DockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> docks;
    private GameObject currentDock;

    private void Awake()
    {
        DeactivateDocks();
    }
    private void OnDisable()
    {
        
    }
    public void DeactivateDocks()
    {
        foreach (GameObject dock in docks)
        {
            dock.SetActive(false);
        }
        currentDock = null;
    }
    public void ActivateRandomDock()
    {
        DeactivateDocks();

        currentDock = docks[Random.Range(0, docks.Count)];
        SetDockAndChildrenActive(currentDock);
    }
    void SetDockAndChildrenActive(GameObject dock)
    {
        dock.SetActive(true);

        foreach (Transform child in dock.transform)
        {
            child.gameObject.SetActive(true);
            dock.GetComponent<BoxCollider>().enabled = true;
        }
    }

}
