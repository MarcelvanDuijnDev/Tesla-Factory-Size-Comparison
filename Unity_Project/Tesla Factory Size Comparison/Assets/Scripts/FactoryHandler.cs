using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactoryHandler : MonoBehaviour
{
    //Profiles
    [Header("Profiles")]
    public List<FactoryProfile> FactoryProfiles = new List<FactoryProfile>();

    [Header("Settings")]
    public Vector2 Scaling;

    [Header("Compare Objects")]
    public List<GameObject> CompareObjects = new List<GameObject>();

    [Header("UI")]
    public TextMeshProUGUI UIText_Scaling;

    private float _Scaling;
    private List<FactoryHandler_Profile> Factory_Profiles = new List<FactoryHandler_Profile>();

    private int Sort_ProfilesBySize(FactoryProfile profile1, FactoryProfile profile2)
    {
        return profile2.SquareMeters.CompareTo(profile1.SquareMeters);
    }

    void Start()
    {
        _Scaling = Scaling.y / Scaling.x;
        FactoryProfiles.Sort(Sort_ProfilesBySize);
        CreatFactories();

        //Scale Text
        UIText_Scaling.text = Scaling.x.ToString() + ":" + Scaling.y.ToString();

        //Compare Objects
        for (int i = 0; i < CompareObjects.Count; i++)
        {
            CompareObjects[i].transform.localScale *= _Scaling;
            CompareObjects[i].transform.position = new Vector3(CompareObjects[i].transform.position.x, CompareObjects[i].transform.localScale.y * 0.5f, CompareObjects[i].transform.position.z);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
            if (FactoryProfiles[i].SquareMeters > 0)
                newfactorymodel.transform.localScale = Vector3.one * (Mathf.Sqrt(FactoryProfiles[i].SquareMeters) * _Scaling);
            else
                newfactorymodel.transform.localScale = Vector3.one;
            newfactorymodel.transform.localScale = new Vector3(newfactorymodel.transform.localScale.x, 3, newfactorymodel.transform.localScale.z);

            Factory_Profiles[i].FactoryObject = newfactory;
            Factory_Profiles[i].FactoryModel = newfactorymodel;

            float textheight = 3.01f;

            //Name
            Factory_Profiles[i].FactoryName = newfactory.transform.GetChild(1).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryName.text = FactoryProfiles[i].Name;
            Factory_Profiles[i].FactoryName.transform.position = new Vector3(newfactory.transform.position.x, textheight, newfactory.transform.position.z);
            Factory_Profiles[i].FactoryName.transform.localScale = new Vector3(newfactorymodel.transform.localScale.x * 0.01f, newfactorymodel.transform.localScale.x * 0.01f, 1);

            //Location
            Factory_Profiles[i].FactoryLocation = newfactory.transform.GetChild(2).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryLocation.text = FactoryProfiles[i].Location;
            Factory_Profiles[i].FactoryLocation.transform.position = new Vector3(newfactory.transform.position.x, textheight, newfactory.transform.position.z - (newfactorymodel.transform.localScale.x * 0.05f));
            Factory_Profiles[i].FactoryLocation.transform.localScale = new Vector3(newfactorymodel.transform.localScale.x * 0.01f, newfactorymodel.transform.localScale.x * 0.01f, 1);

            //OpenDate
            Factory_Profiles[i].FactoryOpenDate = newfactory.transform.GetChild(3).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryOpenDate.text = FactoryProfiles[i].OpenDate.ToString();
            Factory_Profiles[i].FactoryOpenDate.transform.position = new Vector3(newfactory.transform.position.x, textheight, newfactory.transform.position.z - (newfactorymodel.transform.localScale.x * 0.05f)*2);
            Factory_Profiles[i].FactoryOpenDate.transform.localScale = new Vector3(newfactorymodel.transform.localScale.x * 0.01f, newfactorymodel.transform.localScale.x * 0.01f, 1);

            //Employees
            Factory_Profiles[i].FactoryEmployees = newfactory.transform.GetChild(4).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactoryEmployees.text = FactoryProfiles[i].Employees.ToString();
            Factory_Profiles[i].FactoryEmployees.transform.position = new Vector3(newfactory.transform.position.x, textheight, newfactory.transform.position.z - (newfactorymodel.transform.localScale.x * 0.05f) * 3);
            Factory_Profiles[i].FactoryEmployees.transform.localScale = new Vector3(newfactorymodel.transform.localScale.x * 0.01f, newfactorymodel.transform.localScale.x * 0.01f, 1);

            //SquareMeters
            Factory_Profiles[i].FactorySquareMeters = newfactory.transform.GetChild(5).GetComponent<TextMeshPro>();
            Factory_Profiles[i].FactorySquareMeters.text = FactoryProfiles[i].SquareMeters.ToString() + "m²";
            Factory_Profiles[i].FactorySquareMeters.transform.position = new Vector3(newfactory.transform.position.x, textheight, newfactory.transform.position.z - (newfactorymodel.transform.localScale.x * 0.05f) * 4);
            Factory_Profiles[i].FactorySquareMeters.transform.localScale = new Vector3(newfactorymodel.transform.localScale.x * 0.01f, newfactorymodel.transform.localScale.x * 0.01f, 1);

            Factory_Profiles[i].FactoryProfile = FactoryProfiles[i];
            newfactory.transform.SetParent(factoryparentobj.transform);

            Reorder();
        }
    }

    private void Reorder()
    {
        float prevscale = 0;
        float currentpos = 0;

        Factory_Profiles[0].FactoryObject.transform.position = new Vector3(0, Factory_Profiles[0].FactoryObject.transform.localScale.y * 0.5f, 0);

        for (int i = 0; i < Factory_Profiles.Count; i++)
        {
            Factory_Profiles[i].FactoryObject.transform.position = new Vector3(currentpos + (prevscale * 0.5f) + (Factory_Profiles[i].FactoryModel.transform.localScale.x * 0.5f) + 20, Factory_Profiles[i].FactoryObject.transform.localScale.y * 0.5f, 0);
            currentpos = Factory_Profiles[i].FactoryObject.transform.position.x;
            prevscale = Factory_Profiles[i].FactoryModel.transform.localScale.x;
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
        return profile1.FactoryProfile.Name.CompareTo(profile2.FactoryProfile.Name);
    }
}

[System.Serializable]
public class FactoryHandler_Profile
{
    public FactoryProfile FactoryProfile;
    public GameObject FactoryObject;
    public GameObject FactoryModel;
    public TextMeshPro FactoryName;
    public TextMeshPro FactoryLocation;
    public TextMeshPro FactoryOpenDate;
    public TextMeshPro FactoryEmployees;
    public TextMeshPro FactorySquareMeters;
}