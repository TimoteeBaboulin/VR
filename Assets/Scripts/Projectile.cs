using UnityEngine;

public class Projectile : MonoBehaviour{
    [SerializeField] private float _speed;
    private void Update(){
        transform.position += transform.forward * (Time.deltaTime * _speed);
    }
}