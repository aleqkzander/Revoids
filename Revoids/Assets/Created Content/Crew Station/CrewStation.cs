using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewStation : MonoBehaviour
{
    public GameObject crewMember;
    public int memberContent = 3;


    /// <summary>
    /// method will store a definded amount of crew members
    /// </summary>
    public void SpawnCrewMembers()
    {
        for (int i = 0; i < memberContent; i++)
        {
            // spawm crewmembers
            Instantiate(crewMember, gameObject.transform.position, Quaternion.identity);

            // destroy gameobject
            Destroy(gameObject);
        }
    }
}
