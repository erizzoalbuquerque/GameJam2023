using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    List<Table> _tables;
    List<Door> _doors;

    static RoomManager _instance;

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

    // Start is called before the first frame update
    void Awake()
    {
        _tables = new List<Table> (FindObjectsOfType<Table>());
        _doors= new List<Door>(FindObjectsOfType<Door>());
    }

    public Table GetFreeTable()
    {
        for (int i = 0; i < _tables.Count; i++)
        {
            Table candidateTable = _tables[i];

            if (candidateTable.IsFree())
            {
                return candidateTable;
            }
        }

        return null;
    }

    public Door GetDoor()
    {
        return _doors[Random.Range(0, _doors.Count)];
    }
}
