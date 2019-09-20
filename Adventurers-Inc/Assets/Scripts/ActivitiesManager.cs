using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    public static ActivitiesManager instance = null;       //singleton's instance

    public enum Status { Unset, Available, Occupied};

    public GameObject _activities;                  //The gameobject which contains the activities distributed in the scene
    public List<GameObject> _activityPointsList;


    public void Awake()
    {
        //singleton definition
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _activityPointsList = GetChildrenInGameObject(_activities);
    }

    //Returns a list of the gameObjects contained in a target gameobject
    private List<GameObject> GetChildrenInGameObject(GameObject target)
    {
        List<GameObject> result = new List<GameObject>();

        foreach (Transform item in target.transform)
        {
            result.Add(item.gameObject);
        }

        return result;
    }

    //Returns an activity from _activityPointsList among those set as "available"
    public GameObject GetRandomAvailableActivity()
    {
        GameObject result = null;

        //Compose a list of potential results
        List<GameObject> candidates = new List<GameObject>();
        foreach (GameObject item in _activityPointsList)
        {
            if (item.GetComponent<Activity>()._status == ActivitiesManager.Status.Available)
                candidates.Add(item);
        }

        //Pick at random if there are candidates
        if (candidates.Count >= 1)
            result = candidates[Random.Range(0, candidates.Count)];
        else
            Debug.Log("No available activity");

        return result;
    }
}
