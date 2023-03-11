using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamColorChanger : MonoBehaviour
{
    [SerializeField] private Material Blue;
    [SerializeField] private Material Red;
    [SerializeField] private Material Normal;
    [SerializeField] private MeshRenderer[] objectToChange;

    public void ChangeCollor(TeamType type, bool taken)
    {
        if (!taken) 
        {
            for(int i = 0; i < objectToChange.Length; i++)
            {
                objectToChange[i].material = Normal;
			}
            return;
        }
        if(type == TeamType.Blue)
        {
            for (int i = 0; i < objectToChange.Length; i++)
            {
                objectToChange[i].material = Blue;
            }
		}
        if(type == TeamType.Red)
        {
            for (int i = 0; i < objectToChange.Length; i++)
            {
                objectToChange[i].material = Red;
            }
        }   
    }
}
