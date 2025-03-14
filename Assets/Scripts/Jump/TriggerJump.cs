using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TriggerJump : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private AttractionTrigger attractionTrigger;
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsWalk = Animator.StringToHash("IsWalk");

    private void Awake()
    {
        if (GetComponent<BoxCollider>() != null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject character = other.gameObject;
        if (character.CompareTag("Character") && !character.GetComponent<Character>().alreadyJump)
        {
            character.transform.rotation = Quaternion.LookRotation(attractionTrigger.transform.position - character.transform.position);
            character.GetComponent<Character>().alreadyJump = true;
            character.GetComponent<Animator>().SetTrigger(IsJump);
            
            character.transform.DOMoveY(character.transform.position.y + 2f, 0.5f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutQuad);
            
            character.GetComponent<Animator>().SetTrigger(IsWalk);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        GameObject character = other.gameObject;
        if (character.CompareTag("Character") && character.GetComponent<Character>().alreadyJump)
        {
            character.GetComponent<Character>().alreadyJump = false;
        }
    }
}
