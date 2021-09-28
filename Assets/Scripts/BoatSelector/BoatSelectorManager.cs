using Assets.Scripts.BoatSelector;
using Assets.Scripts.Game.Network.Packets.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoatSelectorManager : MonoBehaviour
{

    public static Board Board { get; set; }

    bool _mapOpened = false;
    public BoatIdentifier.Boats Boat { get; set; }

    Dictionary<BoatIdentifier.Boats, GameObject> _mapBoats = new Dictionary<BoatIdentifier.Boats, GameObject>();

    public static Dictionary<BoatIdentifier.Boats, BoatPosition> _mapPlacements = new Dictionary<BoatIdentifier.Boats, BoatPosition>
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
    public string _selectedTag;

    public RectTransform m_parent;
    public Camera m_uiCamera;
    public RectTransform m_image;

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
                _position = Vector2.Lerp(_selectedTransform.position, _mousePosition, MoveSpeed);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Canvas canvas = FindObjectOfType<Canvas>();
                if(canvas != null)
                {
                    Vector2 anchorPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parent, _position, null, out anchorPos);
                    
                    BoatPosition position = new BoatPosition();
                    position.x = anchorPos.x;
                    position.y = anchorPos.y;
                    position.boatName = _selectedTag;
                    _mapPlacements.Remove(Boat);
                    _mapPlacements.Add(Boat, position);
                    //Debug.LogWarning("From top : " + ((m_parent.rect.height / 2) - anchorPos.y));
                    //Debug.LogWarning("From left : " + ((m_parent.rect.width / 2) + anchorPos.x));
                    //Debug.LogWarning("Width : " + m_image.rect.width);
                    //Debug.LogWarning("Height : " + m_image.rect.height);
                    Boat = BoatIdentifier.Boats.EMPTY;
                    ToggleMap();
                }
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
            foreach(KeyValuePair<BoatIdentifier.Boats, GameObject> boats in _mapBoats)
            {
                if(boats.Value != obj)
                {
                    boats.Value.GetComponent<Rigidbody2D>().isKinematic = true;
                } else
                {
                    boats.Value.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }

            _rb = obj.GetComponent<Rigidbody2D>();
            _selectedTransform = obj.transform;
            m_image = obj.GetComponent<RectTransform>();
            _selectedTag = obj.tag;
        }
    }

}
