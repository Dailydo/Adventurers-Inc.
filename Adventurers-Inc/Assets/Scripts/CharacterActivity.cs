using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterActivity : MonoBehaviour
{
    public enum Status { Unset, Moving, PerformingActivity };

    public Status _status = Status.Unset;
    public Activity _targetedActivity;
    public float _currentDistance = 0f;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //If character should move
        if (_status == Status.Moving)
        {
            _currentDistance = Vector3.Distance(transform.position, _targetedActivity.transform.position);
            //If character is close enough to destination
            if (_currentDistance <= Activities.instance._activityProximityTolerance)
            {
                transform.position = _targetedActivity.transform.position;
                _targetedActivity._status = Activities.Status.Occupied;

                //Wait for some time while performing activity then move elsewhere
                _status = Status.PerformingActivity;
                StartCoroutine(WaitRandomDurationThenMove());
            }
        }
    }

    //Moves the character to an available activity in the scene
    public void MoveToAvailableActivity()
    {
        //Signal the activities manager that the character is waiting for an activity
        Activities.instance.LogPendingActivity(this);
    }

    //Sets a destination for the character
    public void SetDestination(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    //Waits for a random duration (min, max) then moves the character elsewhere
    IEnumerator WaitRandomDurationThenMove()
    {
        float activityDuration = Random.Range(Activities.instance._activityMinDuration, Activities.instance._activityMaxDuration);

        yield return new WaitForSeconds(activityDuration);
        MoveToAvailableActivity();
    }
}
