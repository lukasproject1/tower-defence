using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask fieldLayer;  // LayerMask om de 'Field' layer aan te geven
    public LayerMask ignoreLayer; // LayerMask om lagen te negeren boven het veld
    public List<GameObject> towerPrefabs; // Lijst van torenprefabs
    public List<int> towerCosts; // Lijst van kosten voor de torens
    public Camera mainCamera; // De camera waarmee de raycast wordt gemaakt
    public float placementRadius = 1.0f; // Radius om te controleren op bestaande torens

    private List<Vector3> placedTowerPositions = new List<Vector3>();
    private int selectedTowerIndex = 0; // Index van de momenteel geselecteerde toren

    // Start is called before the first frame update
    void Start()
    {
        if (towerPrefabs.Count == 0)
        {
            Debug.LogError("Geen torenprefabs toegewezen!");
        }

        if (towerCosts.Count != towerPrefabs.Count)
        {
            Debug.LogError("Het aantal torenkosten komt niet overeen met het aantal torenprefabs!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleTowerSelection();

        if (Input.GetMouseButtonDown(0)) // Linker muisknop wordt ingedrukt
        {
            PlaceTower();
        }
    }

    void HandleTowerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTowerIndex = 0;
            Debug.Log("Toren 1 geselecteerd");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTowerIndex = 1;
            Debug.Log("Toren 2 geselecteerd");
        }

        // Zorg ervoor dat de geselecteerde index binnen de grenzen van de lijst blijft
        selectedTowerIndex = Mathf.Clamp(selectedTowerIndex, 0, towerPrefabs.Count - 1);
    }

    void PlaceTower()
    {
        int selectedTowerCost = towerCosts[selectedTowerIndex];

        if (MoneySystem.instance.money < selectedTowerCost)
        {
            Debug.Log("Niet genoeg geld om een toren te plaatsen.");
            return;
        }

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
                    // Plaats de geselecteerde toren op de positie van de raycast hit
                    Instantiate(towerPrefabs[selectedTowerIndex], hitPoint, Quaternion.identity);
                    placedTowerPositions.Add(hitPoint);
                    MoneySystem.instance.AddMoney(-selectedTowerCost); // Trek het geld af
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




/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask fieldLayer;  // LayerMask om de 'Field' layer aan te geven
    public LayerMask ignoreLayer; // LayerMask om lagen te negeren boven het veld
    public List<GameObject> towerPrefabs; // Lijst van torenprefabs
    public Camera mainCamera; // De camera waarmee de raycast wordt gemaakt
    public float placementRadius = 1.0f; // Radius om te controleren op bestaande torens
    public int towerCost = 200; // Kosten van een toren

    private List<Vector3> placedTowerPositions = new List<Vector3>();
    private int selectedTowerIndex = 0; // Index van de momenteel geselecteerde toren

    // Start is called before the first frame update
    void Start()
    {
        if (towerPrefabs.Count == 0)
        {
            Debug.LogError("Geen torenprefabs toegewezen!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleTowerSelection();

        if (Input.GetMouseButtonDown(0)) // Linker muisknop wordt ingedrukt
        {
            PlaceTower();
        }
    }

    void HandleTowerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTowerIndex = 0;
            Debug.Log("Toren 1 geselecteerd");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTowerIndex = 1;
            Debug.Log("Toren 2 geselecteerd");
        }

        // Zorg ervoor dat de geselecteerde index binnen de grenzen van de lijst blijft
        selectedTowerIndex = Mathf.Clamp(selectedTowerIndex, 0, towerPrefabs.Count - 1);
    }

    void PlaceTower()
    {
        if (MoneySystem.instance.money < towerCost)
        {
            Debug.Log("Niet genoeg geld om een toren te plaatsen.");
            return;
        }
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
                    // Plaats de geselecteerde toren op de positie van de raycast hit
                    Instantiate(towerPrefabs[selectedTowerIndex], hitPoint, Quaternion.identity);
                    MoneySystem.instance.AddMoney(-towerCost); // Trek het geld af
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
*/
