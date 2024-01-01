using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class Workbench : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _rayDistance = 2f;
    [SerializeField] private GameObject[] _craftableItems;

    private GameObject _currentItemToCraft;
    private ItemInfo _itemInfo;
    private IconInfo _iconInfo;
    private Inventory _inventory;
    private GameObject[] _itemsForCraft;
    private bool _logIsWasted;
    private bool _stoneIsWasted;
    private Vector3 _spawnItemPosition;

    private void Start()
    {
        _currentItemToCraft = _craftableItems[0];
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _spawnItemPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
    }

    private void FixedUpdate()
    {
        Visualize();
    }

    private void Visualize()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _rayDistance))
        {
            if (hit.collider.tag == "Workbench")
            {
                ItemInfoCheck();
                _itemsForCraft = _itemInfo.GetItemsForCraft();
                _text.text = "Current craft: " + _itemInfo.GetItemName();
                CraftItem();
                CurrentItemToCraftChange();
            }
        }

        else
            _text.text = "";
    }

    private void CraftItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < _inventory.cells.Length; i++)
            {
                if (_inventory.isFull[i])
                {
                    _iconInfo = _inventory.cells[i].GetComponentInChildren<IconInfo>();

                    if (_iconInfo.GetIconPrefab() == _itemsForCraft[0] && _logIsWasted == false)
                    {
                        _logIsWasted = true;
                        Destroy(_inventory.cells[i].transform.GetChild(0).gameObject);
                        _inventory.isFull[i] = false;
                    }

                    if (_iconInfo.GetIconPrefab() == _itemsForCraft[1] && _stoneIsWasted == false)
                    {
                        _stoneIsWasted = true;
                        Destroy(_inventory.cells[i].transform.GetChild(0).gameObject);
                        _inventory.isFull[i] = false;
                    }

                    if (_logIsWasted && _stoneIsWasted)
                    {
                        Instantiate<GameObject>(_itemInfo.GetItemPrefab(), _spawnItemPosition, Quaternion.identity);
                        _logIsWasted = false;
                        _stoneIsWasted = false;
                        break;
                    }
                }
            }
        }
    }

    private void CurrentItemToCraftChange()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_currentItemToCraft == _craftableItems[0])
                _currentItemToCraft = _craftableItems[1];

            else if (_currentItemToCraft == _craftableItems[1])
                _currentItemToCraft = _craftableItems[2];

            else if (_currentItemToCraft == _craftableItems[2])
                _currentItemToCraft = _craftableItems[0];
        }
    }

    private void ItemInfoCheck()
    {
        _itemInfo = _currentItemToCraft.GetComponent<ItemInfo>();
    }
}
