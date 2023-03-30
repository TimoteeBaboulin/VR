using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour{
    public static event Action OnDeath;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _headSprites;
    [SerializeField] private ParticleSystem _poof;

    private void Awake(){
        _renderer.sprite = _headSprites[Random.Range(0, _headSprites.Length)];
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Trigger");
        if (other.GetComponent<Projectile>() != null){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy(){
        OnDeath?.Invoke();
        Instantiate(_poof, transform.position, transform.rotation);
    }
}