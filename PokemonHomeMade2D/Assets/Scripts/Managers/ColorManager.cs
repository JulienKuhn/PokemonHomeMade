using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CustomEnums;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    [SerializeField] private List<ColorAssociation> colorByType;

    private void Awake()
    {
        instance = this;
    }

    public Color GetColorByType(PokemonType type)
    {
        return colorByType.Where(c=>c.type == type).FirstOrDefault().color;
    }

    [Serializable]
    private class ColorAssociation
    {
        public PokemonType type;
        public Color color;
    }
}
