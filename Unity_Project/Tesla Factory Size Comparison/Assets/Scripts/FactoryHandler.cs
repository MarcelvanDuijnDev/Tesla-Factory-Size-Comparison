using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactoryHandler : MonoBehaviour
{
    //Profiles
    public List<FactoryProfile> FactoryProfiles = new List<FactoryProfile>();
    private List<FactoryHandler_Profile> Factory_Profiles = new List<FactoryHandler_Profile>();

    private int Sort_ProfilesBySize(FactoryProfile profile1, FactoryProfile profile2)
    {
        return profile2.SquareMeters.CompareTo(profile1.SquareMeters);
    }

    void Start()
    {
        FactoryProfiles.Sort(Sort_ProfilesBySize);
        CreatFactories();
    }

    void CreatFactories()
    {
        GameObject factoryparentobj = new GameObject();
        factoryparentobj.name = "Factories";

        for (int i = 0; i < FactoryProfiles.Count; i++)
        {
            FactoryHandler_Profile newfactoryprofile = new FactoryHandler_Profile();
            Factory_Profiles.Add(newfactoryprofile);

            GameObject newfactory = Instantiate(FactoryProfiles[i].Model);
            newfactory.name = FactoryProfiles[i].Name;

            GameObject newfactorymodel = newfactory.transform.GetChild(0).gameObject;
            newfactorymodel.transform.localScale = Vector3.one * (FactoryProfiles[i].SquareMeters * 0.0001f);
            newfactorymodel.transform.localScale = new Vector3(newfactorymodel.transform.localScale.x, 3, newfactorymodel.transform.localScale.z);

            newfactory.transform.position = new Vector3(i * 100, newfactorymodel.transform.localScale.y * 0.5f, 0);

            Factory_Profiles[i].FactoryObject = newfactory;

            //Name
            Factory_Profiles[i].FactoryName = newfactory.transform.GetChild(1).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryName.text = FactoryProfiles[i].Name;
            Factory_Profiles[i].FactoryName.transform.position = new Vector3(newfactory.transform.position.x, 5, newfactory.transform.position.z);

            //Location
            Factory_Profiles[i].FactoryLocation = newfactory.transform.GetChild(2).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryLocation.text = FactoryProfiles[i].Location;
            Factory_Profiles[i].FactoryLocation.transform.position = new Vector3(newfactory.transform.position.x, 5, newfactory.transform.position.z - 5);

            //OpenDate
            Factory_Profiles[i].FactoryOpenDate = newfactory.transform.GetChild(3).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryOpenDate.text = FactoryProfiles[i].OpenDate.ToString();
            Factory_Profiles[i].FactoryOpenDate.transform.position = new Vector3(newfactory.transform.position.x, 5, newfactory.transform.position.z - 10);

            //Employees
            Factory_Profiles[i].FactoryEmployees = newfactory.transform.GetChild(4).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryEmployees.text = FactoryProfiles[i].Employees.ToString();
            Factory_Profiles[i].FactoryEmployees.transform.position = new Vector3(newfactory.transform.position.x, 5, newfactory.transform.position.z - 15);

            //SquareMeters
            Factory_Profiles[i].FactorySquareMeters = newfactory.transform.GetChild(5).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactorySquareMeters.text = FactoryProfiles[i].SquareMeters.ToString() + "m²";
            Factory_Profiles[i].FactorySquareMeters.transform.position = new Vector3(newfactory.transform.position.x, 5, newfactory.transform.position.z - 20);

            Factory_Profiles[i].FactoryProfile = FactoryProfiles[i];
            newfactory.transform.SetParent(factoryparentobj.transform);
        }
    }

    private void Reorder()
    {
        for (int i = 0; i < Factory_Profiles.Count; i++)
        {
            Factory_Profiles[i].FactoryObject.transform.position = new Vector3(i * 100, Factory_Profiles[i].FactoryObject.transform.localScale.y * 0.5f, 0);
        }
    }

    public void SortProfiles(string sorttype)
    {
        switch(sorttype)
        {
            case "name":
                Factory_Profiles.Sort(Sort_ByName);
                break;
            case "size":
                Factory_Profiles.Sort(Sort_BySize);
                break;
            case "employees":
                Factory_Profiles.Sort(Sort_ByEmployees);
                break;
            case "opendate":
                Factory_Profiles.Sort(Sort_ByOpenDate);
                break;
        }

        Reorder();
    }

    private int Sort_BySize(FactoryHandler_Profile profile1, FactoryHandler_Profile profile2)
    {
        return profile2.FactoryProfile.SquareMeters.CompareTo(profile1.FactoryProfile.SquareMeters);
    }
    private int Sort_ByEmployees(FactoryHandler_Profile profile1, FactoryHandler_Profile profile2)
    {
        return profile2.FactoryProfile.Employees.CompareTo(profile1.FactoryProfile.Employees);
    }
    private int Sort_ByOpenDate(FactoryHandler_Profile profile1, FactoryHandler_Profile profile2)
    {
        return profile2.FactoryProfile.OpenDate.CompareTo(profile1.FactoryProfile.OpenDate);
    }
    private int Sort_ByName(FactoryHandler_Profile profile1, FactoryHandler_Profile profile2)
    {
        return profile2.FactoryProfile.Name.CompareTo(profile1.FactoryProfile.Name);
    }
}

[System.Serializable]
public class FactoryHandler_Profile
{
    public FactoryProfile FactoryProfile;
    public GameObject FactoryObject;
    public TextMeshPro FactoryName;
    public TextMeshPro FactoryLocation;
    public TextMeshPro FactoryOpenDate;
    public TextMeshPro FactoryEmployees;
    public TextMeshPro FactorySquareMeters;
}