using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum FavouriteAttraction
{
    NEUTRAL = 0,
    ATTRACTION1 = 1,
    ATTRACTION2 = 2,
    ATTRACTION3 = 3
}

public class Character : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float baseSpeed = 5f;
    [SerializeField]
    private float runSpeed = 10f;
    private Transform currentWaypoint;
    private int currentWaypointID;
    private bool isMoving = false;
    private Transform[] waypoints;
    private Transform leaveWaypoint;

    [SerializeField]
    private FavouriteAttraction favouriteAttraction = FavouriteAttraction.NEUTRAL;


    void Start()
    {
        //DEV
        //StartCoroutine(Delete(7f));
        speed = baseSpeed;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveToWaypoint();
        }
    }

    private void MoveToWaypoint()
    {
        Vector3 direction = currentWaypoint.position - transform.position;
        direction = direction.normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(currentWaypoint.position, transform.position) < 0.2f)
        {
            transform.position = currentWaypoint.position;

            if (waypoints != null && currentWaypointID != -1)
            {
                //Si on est pas pass� par tempWaypoint
                if (currentWaypoint.transform.position == waypoints[currentWaypointID].transform.position)
                {
                    isMoving = false;
                }
                else
                {
                    currentWaypoint = waypoints[currentWaypointID];
                }
            }
        }
    }

    public void SetRandomFavouriteAttraction()
    {
        favouriteAttraction = (FavouriteAttraction)Random.Range(1, 4);

        Debug.Log("New favourite attraction :" + favouriteAttraction);
    }

    public void SetWaypoint(Transform waypoint, int id)
    {
        currentWaypoint = waypoint;
        currentWaypointID = id;
        isMoving = true;
    }

    public int GetWaypointID()
    {
        return currentWaypointID;
    }

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
    }

    public void Reset()
    {
        currentWaypoint = null;
        currentWaypointID = 0;
        isMoving = false;
        waypoints = null;
        speed = baseSpeed;
    }

    public void AddToPulling()
    {
        Pulling pulling = FindAnyObjectByType<Pulling>();
        if (pulling != null)
        {
            pulling.AddCharacter(this);
        }
    }

    public void SetLeaveWaypoint(Transform waypoint)
    {
        leaveWaypoint = waypoint;
    }

    public void Leave()
    {
        SetWaypoint(leaveWaypoint, -1);
        speed = runSpeed;
    }

    public FavouriteAttraction GetFavouriteAttraction()
    {
        return favouriteAttraction;
    }
    //DEV
    IEnumerator Delete(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject.FindAnyObjectByType<Queue>().RemoveCharacterFromQueue(this);
        Destroy(gameObject);
    }
}