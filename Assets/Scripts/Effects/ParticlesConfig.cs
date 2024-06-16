using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = nameof(ParticlesConfig), menuName = "Effects/" + nameof(ParticlesConfig), order = 1)]
public class ParticlesConfig : ScriptableObject
{
    [SerializeField] private List<ParticlesHolder> _particles;

    public List<ParticlesHolder> Particles =>
        _particles;
}

[Serializable]
public class ParticlesHolder
{
    public GameObject Particle;
    public SlotType Type;
}
