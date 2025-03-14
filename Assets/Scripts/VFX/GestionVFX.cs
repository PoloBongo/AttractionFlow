using System.Collections.Generic;
using UnityEngine;

public class GestionVFX : MonoBehaviour
{
    [Header("Property")] [SerializeField] private List<ParticleSystem> ParticleSystems;

    // passe en parametre le gameobject qui lance le vfx pour setup le vfx à l'emplacement de la bonne attraction
    public void PlayVFX(int _index, GameObject _parentObject)
    {
        ParticleSystems[_index].gameObject.transform.position = _parentObject.gameObject.transform.position;
        ParticleSystems[_index].Play();
    }
}
