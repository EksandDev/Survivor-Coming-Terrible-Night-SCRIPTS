using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _distance = 35f;

    private GameObject _pickedItem;
    private Inventory _inventory;
    private ItemInfo _itemInfo;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickUp();
    }

    private void PickUp()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _distance)) 
        {
            if (hit.transform.tag == "PickupableItem")
            {
                for (int i = 0; i < _inventory.cells.Length; i++)
                {
                    if (_inventory.isFull[i] == false)
                    {
                        _inventory.isFull[i] = true;
                        _pickedItem = hit.transform.gameObject;
                        _itemInfo = hit.transform.GetComponent<ItemInfo>();
                        Vector3 cellPosition = _inventory.cells[i].transform.position;
                        
                        GameObject itemInInventory;
                        itemInInventory = Instantiate(_itemInfo.GetItemIcon(), _inventory.cells[i].transform);
                        itemInInventory.transform.position = cellPosition;
                        itemInInventory.SetActive(true);
                        Destroy(_pickedItem);
                        break;
                    }
                }
            }
        }
    }
}
