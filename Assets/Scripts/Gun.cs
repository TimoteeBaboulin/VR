using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour, IUseable{
    [SerializeField] private Transform _canon;
    [SerializeField] private GameObject _projectilePrefab;
    
    [SerializeField] private AudioClip[] _shotSfx;
    [SerializeField] private GameObject[] _muzzleFlashes;

    private GameObject _currentMuzzleFlash;
    private Transform _handTransform;
    private AudioSource _source;
    private Animator _animator;

    private void Awake(){
        _source = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void Update(){
        if (_handTransform == null) return;
        transform.position = _handTransform.position;
        transform.rotation = _handTransform.rotation;
    }

    public void Interact(Player player, int hand){
        _handTransform = player.GetHandTransform(hand);
        player.SetUseable(this, hand);
    }
    
    [ContextMenu("Shoot")]
    public void Use(){
        // if (_handTransform == null) return;
        if (_projectilePrefab == null) return;
        Instantiate(_projectilePrefab, _canon.position, transform.rotation);
        _animator.Play("Gun Animation", 0, 0);
        PlayFiringSound();
    }

    public void SpawnMuzzleFlash(){
        if (_currentMuzzleFlash != null && _currentMuzzleFlash.activeInHierarchy) _currentMuzzleFlash.SetActive(false); 
        var random = Random.Range(0, _muzzleFlashes.Length);

        _currentMuzzleFlash = _muzzleFlashes[random];
        _currentMuzzleFlash.SetActive(true);
    }
    
    public void KillMuzzleFlash(){
        _currentMuzzleFlash.SetActive(false);
    }
    
    private void PlayFiringSound(){
        int random = Random.Range(0, _shotSfx.Length - 1);

        _source.clip = _shotSfx[random];
        _source.time = 0;
        _source.Play();
    }
}

public interface IUseable : IInteractable{
    public void Use();
}

public interface IInteractable{
    public void Interact(Player player, int hand);
}