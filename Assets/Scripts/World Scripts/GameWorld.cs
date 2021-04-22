using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New World", menuName = "Game World/New World")]
public class GameWorld : ScriptableObject
{
    public Texture2D previewImage;
    public int CurrentLevelIndex = 0;
    public List<WorldLevel> worldLevels { get; }
}
