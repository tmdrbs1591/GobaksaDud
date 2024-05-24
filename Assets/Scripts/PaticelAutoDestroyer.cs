using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PaticelAutoDestroyer : MonoBehaviour
{

    private ParticleSystem particle;
    // Start is called before the first frame update
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particle.isPlaying == false)
        {
            Destroy(gameObject);
        }

    }
}
