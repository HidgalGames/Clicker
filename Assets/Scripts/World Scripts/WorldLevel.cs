using System.Collections.Generic;
using UnityEngine;

public class WorldLevel : ScriptableObject
{
    public Texture2D previewImage;
    [SerializeField] private List<Location> levelLocations;
}
