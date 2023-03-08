using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamColorChanger : MonoBehaviour
{
    [SerializeField] private Material Blue;
    [SerializeField] private Material Red;
    [SerializeField] private Material Normal;
    [SerializeField] private MeshRenderer objectToChange;

    public void ChangeCollor(TeamType type, bool taken)
    {
        if (!taken) 
        { 
            objectToChange.material = Normal; 
            return; 
        }
        if(type == TeamType.Blue)
        {
            objectToChange.material = Blue;
		}
        if(type == TeamType.Red)
        {
            objectToChange.material = Red;
        }   
    }
}
