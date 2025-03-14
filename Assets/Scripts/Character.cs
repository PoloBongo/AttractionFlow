using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum Emotion
{
    NEUTRAL,
    IMPATIENT,
}

enum FavouriteAttraction
{
    NEUTRAL,
    ATTRACTION1,
    ATTRACTION2,
    ATTRACTION3
}

public class Character : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 5f;
    private Transform currentWaypoint;
    private int currentWaypointID;
    private bool isMoving = false;
    private Transform[] waypoints;

    [SerializeField]
    private Emotion currentEmotion = Emotion.NEUTRAL;
    private FavouriteAttraction favouriteAttraction = FavouriteAttraction.NEUTRAL;

    void Start()
    {
        //DEV
        //StartCoroutine(Delete(7f));
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

            if (waypoints != null)
            {
                //Si on est pas passé par tempWaypoint
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

    //DEV
    IEnumerator Delete(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject.FindAnyObjectByType<Queue>().RemoveCharacterFromQueue(this);
        Destroy(gameObject);
    }
}
