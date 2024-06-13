using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask fieldLayer;  // LayerMask om de 'Field' layer aan te geven
    public LayerMask ignoreLayer; // LayerMask om lagen te negeren boven het veld
    public GameObject towerPrefab; // Prefab van de toren die geplaatst moet worden
    public Camera mainCamera; // De camera waarmee de raycast wordt gemaakt
    public float placementRadius = 1.0f; // Radius om te controleren op bestaande torens

    private List<Vector3> placedTowerPositions = new List<Vector3>();

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
                    Vector3 hitPoint = hit.point;

                    if (CanPlaceTower(hitPoint))
                    {
                        // Plaats de toren op de positie van de raycast hit
                        Instantiate(towerPrefab, hitPoint, Quaternion.identity);
                        placedTowerPositions.Add(hitPoint);
                        Debug.Log("Toren geplaatst op: " + hitPoint);
                    }
                    else
                    {
                        Debug.Log("Kan geen toren plaatsen op dezelfde plaats.");
                    }
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

    bool CanPlaceTower(Vector3 position)
    {
        foreach (var placedPosition in placedTowerPositions)
        {
            if (Vector3.Distance(placedPosition, position) < placementRadius)
            {
                return false;
            }
        }
        return true;
    }

    bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}

