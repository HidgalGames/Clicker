using System.Collections.Generic;
using UnityEngine;

public class HeroSquad : MonoBehaviour
{
    [SerializeField] private List<DamageDealer> squad;
    public const int SquadSize = 4;

    public void AddHero(DamageDealer hero)
    {
        if(squad.Count < SquadSize)
        {
            squad.Add(hero);
        }
    }

    public void ChangeHeroAtIndex(int index, DamageDealer hero)
    {
        if (squad[index])
        {
            squad[index] = hero;
        }
    }

    private List<GameObject> GetGameObjectList()
    {
        List<GameObject> result = new List<GameObject>();
        foreach(DamageDealer hero in squad)
        {
            if (hero.gameObject)
            {
                result.Add(hero.gameObject);
            }
        }

        return result;
    }
}
