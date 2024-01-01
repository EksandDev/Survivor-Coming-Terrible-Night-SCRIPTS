using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSwitch : MonoBehaviour
{
    private Inventory _inventory;
    private bool[] _isSelected = new bool[5];
    private int _selectedCell = 0;
    private Vector3 _usualScale = new Vector3(1, 1, 1);
    private Vector3 _selectScale = new Vector3(1.2f, 1.2f, 1f);
    private ItemCreator _creator;
    private GameObject _itemInHand;
    private IconInfo _iconInfo;
    private GameObject _hand;
    private GameObject _currentIcon;
    private PlayerStats _playerStats;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
        _hand = GameObject.FindGameObjectWithTag("Hand");
        _playerStats = GetComponent<PlayerStats>();
        _isSelected[0] = true;
    }

    private void Update()
    {
        Switch();
        SelectEffect();
        DrawItemInHand();
        Drop();
        Eat();
    }

    private void Switch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SelectChange(0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectChange(1);
        }
        
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectChange(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectChange(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectChange(4);
        }
    }

    private void SelectEffect()
    {
        if (_isSelected[_selectedCell] == true)
            _inventory.cells[_selectedCell].transform.localScale = _selectScale;

        for (int i = 0; i < _isSelected.Length; i++)
        {
            if (_isSelected[i] == false)
            {
                _inventory.cells[i].transform.localScale = _usualScale;
            }
        }
    }

    private void SelectChange(int index)
    {
        for (int i = 0; i < _isSelected.Length; i++)
            _isSelected[i] = false;

        _isSelected[index] = true;
        _selectedCell = index;
    } 

    private void DrawItemInHand()
    {
        for (int i = 0; i < _isSelected.Length; i++)
            if (_isSelected[i] == true)
            {
                Destroy(_itemInHand);

                if (_inventory.cells[i].transform.childCount != 0)
                {
                    _iconInfo = _inventory.cells[i].gameObject.GetComponentInChildren<IconInfo>();
                    _itemInHand = Instantiate(_iconInfo.GetIconPrefab(), _hand.transform);
                    _itemInHand.GetComponent<Rigidbody>().isKinematic = true;
                    _itemInHand.transform.localPosition = Vector3.zero;
                    _itemInHand.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
                    break;
                }
            }
    }

    private void Drop()
    {
        for (int i = 0; i < _isSelected.Length; i++)
            if (Input.GetKeyDown(KeyCode.G) && _isSelected[i] == true)
            {
                _currentIcon = _inventory.cells[i].gameObject.transform.GetChild(0).gameObject;
                Destroy(_currentIcon);
                _inventory.isFull[i] = false;
                _itemInHand.transform.parent = null;
                _itemInHand.GetComponent<Rigidbody>().isKinematic = false;
                _itemInHand = null;
            }
    }

    private void Eat()
    {
        for (int i = 0; i < _isSelected.Length; i++)
        {
            if (Input.GetMouseButtonDown(0) && _isSelected[i] == true && _itemInHand.TryGetComponent<IEatable>(out IEatable _eatable))
            {
                _currentIcon = _inventory.cells[i].gameObject.transform.GetChild(0).gameObject;
                Destroy(_currentIcon);
                _inventory.isFull[i] = false;
                _itemInHand.transform.parent = null;
                Destroy(_itemInHand);
                _itemInHand = null;
                _playerStats.AddPlayerHunger(_eatable.Satiety);
            }
        }
    }
}
