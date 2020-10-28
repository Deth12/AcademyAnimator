using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class JumpingCube : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private ParticleSystem _landingEffect = default;

    private Material _currentMaterial; 
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.Play("Jump");
        }
    }
    
    public void PlayLandingEffect()
    {
        Debug.Log("[Animation Event] Play landing effect");
        _landingEffect.Play();
    }

    public void ChangerColor()
    {
        _currentMaterial.color = Random.ColorHSV();
    }
}
