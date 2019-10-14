using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RoomScriptableObject", order = 1)]
public class RoomScriptableObject : ScriptableObject
{
    public int currentRoom = 0;
    public string[] roomNames;
    public int[] numGhostsInRoom;
}