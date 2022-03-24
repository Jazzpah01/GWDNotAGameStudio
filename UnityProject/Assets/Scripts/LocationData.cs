using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Assets/Locations")]
public class LocationData : ScriptableObject
{
    public List<LocationLandscapeAxis> landscapeAxis = new List<LocationLandscapeAxis>();

    public int LandscapeAxisWidth => landscapeAxis.Count;
    public int BiomeAxisWidth => landscapeAxis[0].biomeAxis.Count;

    public Location this[int x, int y]
    {
        get
        {
            return landscapeAxis[x].biomeAxis[y];
        }
        set
        {
            landscapeAxis[x].biomeAxis[y] = value;
        }
    }
}