using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float maxY;
    [SerializeField]
    private float minY;
    [SerializeField]
    private GameObject moleHitEffectPrefab;

    [SerializeField]
    private ObjectDetector objectDetector;
    private MoveMent3D moveMent3D;

    void Awake()
    {
        moveMent3D = GetComponent<MoveMent3D>();

        objectDetector.raycastEvent.AddListener(OnHit);
    }

    private void OnHit(Transform target)
    {
        MoleFSM mole = target.GetComponent<MoleFSM>();

        if (mole.MoleState == MoleState.UnderGround) return;

        transform.position = new Vector3(target.position.x,minY,target.position.z);

        mole.ChangeState(MoleState.UnderGround);

        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.1f);

        GameObject clone = Instantiate(moleHitEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem.MainModule main = clone.GetComponent<ParticleSystem>().main;
        main.startColor = mole.GetComponent<MeshRenderer>().material.color;

        StartCoroutine("MoveUp");
    }
    IEnumerator MoveUp()
    {
        moveMent3D.MoveTo(Vector3.up);

        while(true)
        {
            if (transform.position.y >= maxY)
            {
                moveMent3D.MoveTo(Vector3.zero);

                break;
            }

            yield return null;
        }
    }
    void Update()
    {
        
    }
}
