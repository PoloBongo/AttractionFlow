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
    private Transform tempWaypoint;
    private int currentWaypointID;
    private bool isMoving = false;
    private Transform leaveWaypoint;
    [SerializeField]
    private CharacterFavourite favouriteAttractionUI;

    [SerializeField]
    private FavouriteAttraction favouriteAttraction = FavouriteAttraction.NEUTRAL;

    public bool alreadyJump = false;

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
        if (tempWaypoint == null)
        {
            Vector3 direction = currentWaypoint.position - transform.position;
            direction = direction.normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(currentWaypoint.position, transform.position) < 0.2f)
            {
                transform.position = currentWaypoint.position;
                isMoving = false;
            }
        }
        else
        {
            Vector3 direction = tempWaypoint.position - transform.position;
            direction = direction.normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(tempWaypoint.position, transform.position) < 0.2f)
            {
                transform.position = tempWaypoint.position;
                tempWaypoint = null;
            }
        }
    }

    public void SetRandomFavouriteAttraction()
    {
        if (favouriteAttractionUI != null)
        {
            favouriteAttraction = (FavouriteAttraction)Random.Range(1, 4);
            favouriteAttractionUI.gameObject.SetActive(true);
            favouriteAttractionUI.UpdateUI(favouriteAttraction);
            Debug.Log("New favourite attraction :" + favouriteAttraction);
        }
    }

    public void SetWaypoint(Transform waypoint, int id, bool temp = false)
    {
        if (temp)
        {
            tempWaypoint = waypoint;
        }
        else
        {
            currentWaypoint = waypoint;
            currentWaypointID = id;
        }
        isMoving = true;
    }

    public int GetWaypointID()
    {
        return currentWaypointID;
    }

    public void Reset()
    {
        currentWaypoint = null;
        tempWaypoint = null;
        currentWaypointID = 0;
        isMoving = false;
        speed = baseSpeed;
        GetComponentInChildren<CharacterEmoji>().Reset();
        GetComponent<CharacterMood>().SetEmotion(Emotion.NEUTRAL);
        if (favouriteAttractionUI != null)
        {
            favouriteAttractionUI.Reset();
        }
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

    public IEnumerator WaitAndDelete()
    {
        yield return new WaitForSeconds(1.0f);
        AddToPulling();
    }

    //DEV
    IEnumerator Delete(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject.FindAnyObjectByType<Queue>().RemoveCharacterFromQueue(this);
        Destroy(gameObject);
    }
}