using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryHandler : MonoBehaviour
{

    public List<FactoryProfile> FactoryProfiles = new List<FactoryProfile>();


    void Start()
    {
        GameObject factoryparentobj = new GameObject();
        factoryparentobj.name = "Factories";

        int spacebetween = 0;
        for (int i = 0; i < FactoryProfiles.Count; i++)
        {
            if(FactoryProfiles[i].SquareMeters > 0)
            {
                GameObject newfactory = Instantiate(FactoryProfiles[i].Model);
                newfactory.name = FactoryProfiles[i].Name;
                newfactory.transform.localScale = Vector3.one * (FactoryProfiles[i].SquareMeters * 0.0001f);
                newfactory.transform.localScale = new Vector3(newfactory.transform.localScale.x,3, newfactory.transform.localScale.z);
                newfactory.transform.position = new Vector3(spacebetween * 100, newfactory.transform.localScale.y * 0.5f, 0);

                newfactory.transform.SetParent(factoryparentobj.transform);
                spacebetween++;
            }
        }
    }
}
