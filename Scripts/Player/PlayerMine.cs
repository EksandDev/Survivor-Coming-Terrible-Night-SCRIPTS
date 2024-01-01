using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMine : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distance = 2f;

    private ItemInfo _itemInfo;
    private GameObject _itemInHand;
    private GameObject _targetObject;
    private float _objectDamage;
    private bool _canMine = true;
    private Animator _handAnimator;
    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        ObjectHit();
    }

    private void ObjectHit()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _distance))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.TryGetComponent<IMineable>(out IMineable mineable)
                && _playerStats.GetPlayerEnergy() >= 10)
            {
                CheckItemInHand();
                
                if (_objectDamage > 0 && _canMine)
                {
                    _canMine = false;
                    _handAnimator.SetTrigger("objectSmashing");
                    Invoke(nameof(CheckRayOnObject), 2.5f);
                }
            }
        }
    }

    private void CheckItemInHand()
    {
        _itemInHand = _camera.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        _itemInfo = _itemInHand.GetComponent<ItemInfo>();
        _objectDamage = _itemInfo.GetObjectDamage();
        _handAnimator = _camera.transform.GetChild(0).GetComponent<Animator>();
    }

    private void CheckRayOnObject()
    {
        RecoverPossibilityMine();
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        _playerStats.PlayerWasteEnergy(10);

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _distance))
        {
            _targetObject = hit.transform.gameObject;

            if (hit.collider.TryGetComponent<IMineable>(out IMineable mineable))
            {
                CheckItemInHand();
                mineable.Destruction(_objectDamage);
                Debug.LogFormat("Object HP: {0}", mineable.Health);
            }
        }
    }

    private void RecoverPossibilityMine()
    {
        _canMine = true;
    }
}
