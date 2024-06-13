using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask fieldLayer;  // LayerMask om de 'Field' layer aan te geven
    public LayerMask ignoreLayer; // LayerMask om lagen te negeren boven het veld
    public GameObject towerPrefab; // Prefab van de toren die geplaatst moet worden
    public Camera mainCamera; // De camera waarmee de raycast wordt gemaakt

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Linker muisknop wordt ingedrukt
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, ~ignoreLayer);

            // Sorteer de hits op afstand, zodat de dichtsbijzijnde eerst komt
            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (var hit in hits)
            {
                if (IsLayerInLayerMask(hit.collider.gameObject.layer, fieldLayer))
                {
                    // Plaats de toren op de positie van de raycast hit
                    Instantiate(towerPrefab, hit.point, Quaternion.identity);
                    Debug.Log("Toren geplaatst op: " + hit.point);
                    return; // Stop de loop na het plaatsen van de toren
                }
                else
                {
                    Debug.Log("Raakte een object op een andere laag: " + hit.collider.gameObject.layer);
                }
            }
            Debug.Log("Geen geschikt veld gevonden om een toren te plaatsen.");
        }
    }

    bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}

