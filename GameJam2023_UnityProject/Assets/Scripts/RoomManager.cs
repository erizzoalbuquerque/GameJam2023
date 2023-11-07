using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    static RoomManager _instance;

    List<Table> _tables;
    List<Door> _doors;

    [SerializeField] int _maxNumberOfCustomersAtSameTime;

    public static RoomManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RoomManager>();

                if (_instance == null)
                {
                    Debug.LogError("Couldn't find a instance of type RoomManager on the scene!");
                }
            }

            return _instance;
        }
    }

    public int MaxNumberOfCustomersAtSameTime {
        get => _maxNumberOfCustomersAtSameTime;
        set => _maxNumberOfCustomersAtSameTime = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _tables = new List<Table> (FindObjectsOfType<Table>(true));
        _doors= new List<Door>(FindObjectsOfType<Door>());
    }

    public Table GetFreeTable()
    {
        List<Table> freeTables = _tables.Where(x => x.IsFree() && x.IsActive()).ToList();
        List<Table> activeTables = _tables.Where(x => x.IsActive()).ToList();

        int numberOfOccupiedTables = activeTables.Count - freeTables.Count;

        if (numberOfOccupiedTables >= _maxNumberOfCustomersAtSameTime)
        {
            return null;
        }
        else
        {
            return activeTables[Random.Range(0, activeTables.Count)];
        }
    }

    public Door GetDoor()
    {
        return _doors[Random.Range(0, _doors.Count)];
    }

    public void EnableNewTable()
    {
        List<Table> inactiveTables = _tables.Where(x => x.IsActive() == false).ToList();

        if (inactiveTables.Count > 0)
        {
            inactiveTables[0].gameObject.SetActive(true);
        }    
    }
}
