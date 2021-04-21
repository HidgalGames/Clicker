using System.Collections.Generic;
using UnityEngine;

public class GameWorld : ScriptableObject
{
    public Texture2D previewImage;
    [SerializeField] private List<WorldLevel> worldLevels;
}
