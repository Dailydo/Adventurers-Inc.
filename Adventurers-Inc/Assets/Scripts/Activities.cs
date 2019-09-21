using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activities : MonoBehaviour
{
    public static Activities instance = null;       //singleton's instance
    public enum Status { Unset, Available, Reserved, Occupied};

    public List<Activity> _activityPointsList;
    public float _activityMinDuration = 1f;         //Minimum duration of an activity, in seconds
    public float _activityMaxDuration = 6f;
    public float _activityProximityTolerance = 0.1f;            //Distance from which the character is considered having reached its targeted activity

    private List<CharacterActivity> _characterPendingActivityList;


    public void Awake()
    {
        //singleton definition
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _activityPointsList = GetActivitiesInGameObject(gameObject);
        _characterPendingActivityList = new List<CharacterActivity>();
    }

    public void Update()
    {
        //If characters in the list
        if (_characterPendingActivityList.Count > 0)
        {
            //For each characterin the list
            foreach (CharacterActivity characterActivity in _characterPendingActivityList)
            {
                //Clean current activity and set a new one
                Activity newActivity = GetRandomAvailableActivity();
                characterActivity._targetedActivity._status = Status.Available;
                characterActivity._targetedActivity = newActivity;
                characterActivity._targetedActivity._status = Status.Reserved;

                //Set the character in movement
                characterActivity._status = CharacterActivity.Status.Moving;
                characterActivity.GetComponent<CharacterActivity>().SetDestination(characterActivity._targetedActivity.transform.position);
            }

            //Clear the list
            _characterPendingActivityList.Clear();
        }
    }

    //Returns a list of activity contained in a target gameobject activities
    private List<Activity> GetActivitiesInGameObject(GameObject target)
    {
        List<Activity> result = new List<Activity>();

        foreach (Transform item in target.transform)
        {
            if (item.GetComponent<Activity>())
                result.Add(item.gameObject.GetComponent<Activity>());
        }

        return result;
    }

    //Returns an activity from _activityPointsList among those set as "available"
    public Activity GetRandomAvailableActivity()
    {
        Activity result = null;

        //Compose a list of potential results
        List<Activity> candidates = new List<Activity>();
        foreach (Activity item in _activityPointsList)
        {
            if (item.GetComponent<Activity>()._status == Activities.Status.Available)
                candidates.Add(item);
        }

        //Pick at random if there are candidates
        if (candidates.Count >= 1)
            result = candidates[Random.Range(0, candidates.Count)];
        else
            Debug.Log("No available activity");

        return result;
    }

    //Adds the character activity to the list of those pending one
    public void LogPendingActivity(CharacterActivity characterActivity)
    {
        _characterPendingActivityList.Add(characterActivity);          
    }
}
