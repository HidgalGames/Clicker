using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New World", menuName = "Game World/New Level")]
public class WorldLevel : ScriptableObject
{
    public Texture2D previewImage;
    public int CurrentLocationIndex = 0;
    public List<Location> levelLocations { get; }

    //public delegate void LocationEntered();
    //public event LocationEntered LocationEnteredEvent;    
}
