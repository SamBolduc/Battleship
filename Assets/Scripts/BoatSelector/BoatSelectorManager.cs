using Assets.Scripts.BoatSelector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoatSelectorManager : MonoBehaviour
{

    bool _mapOpened = false;
    public BoatIdentifier.Boats Boat { get; set; }

    Dictionary<BoatIdentifier.Boats, GameObject> _mapBoats = new Dictionary<BoatIdentifier.Boats, GameObject>();

    Dictionary<BoatIdentifier.Boats, Vector2?> _mapPlacements = new Dictionary<BoatIdentifier.Boats, Vector2?>
    {
        { BoatIdentifier.Boats.CARRIER, null },
        { BoatIdentifier.Boats.BATTLESHIP, null },
        { BoatIdentifier.Boats.CRUISER, null },
        { BoatIdentifier.Boats.SUBMARINE, null },
        { BoatIdentifier.Boats.DESTROYER, null }
    };

    GameObject _map;

    Vector3 _mousePosition;
    public float MoveSpeed = 0.1f;
    Rigidbody2D _rb;
    Vector2 _position = new Vector2(0f, 0f);
    Transform _selectedTransform;

    private void Start()
    {
        _mapBoats.Add(BoatIdentifier.Boats.CARRIER, GameObject.FindGameObjectWithTag("CARRIER"));
        _mapBoats.Add(BoatIdentifier.Boats.BATTLESHIP, GameObject.FindGameObjectWithTag("BATTLESHIP"));
        _mapBoats.Add(BoatIdentifier.Boats.CRUISER, GameObject.FindGameObjectWithTag("CRUISER"));
        _mapBoats.Add(BoatIdentifier.Boats.SUBMARINE, GameObject.FindGameObjectWithTag("SUBMARINE"));
        _mapBoats.Add(BoatIdentifier.Boats.DESTROYER, GameObject.FindGameObjectWithTag("DESTROYER"));

        _map = GameObject.FindGameObjectWithTag("MAP");
        if(_map != null)
        {
            _map.SetActive(false);
        }
    }

    private void Update()
    {
        if(_mapOpened)
        {
            if (_rb != null && _selectedTransform != null)
            {
                _mousePosition = Input.mousePosition;
                //_mousePosition = Camera.main.ScreenToViewportPoint(_mousePosition);
                _position = Vector2.Lerp(_selectedTransform.position, _mousePosition, MoveSpeed);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Boat = BoatIdentifier.Boats.EMPTY;
                ToggleMap();
            }
        }
    }

    private void FixedUpdate()
    {
        if(_mapOpened && _rb != null)
        {
           _rb.MovePosition(_position);
        }
    }

    public void ToggleMap()
    {
        if (Boat == BoatIdentifier.Boats.EMPTY)
        {
            _mapOpened = false;
            _map.SetActive(false);
        } else
        {
            _mapOpened = true;
            _map.SetActive(true);
        }
    }

    public void ActiveBoatPlacement(BoatIdentifier.Boats boat)
    {
        Boat = boat;
        ToggleMap();
        GameObject obj;
        _mapBoats.TryGetValue(boat, out obj);

        if(obj != null)
        {
            _rb = obj.GetComponent<Rigidbody2D>();
            _selectedTransform = obj.transform;
        }
    }

}
