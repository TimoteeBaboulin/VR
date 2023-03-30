using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetManager : MonoBehaviour{
    private static int _killCount;

    private int _targetCount;
    
    [SerializeField] private int _startingTargetCount;
    [SerializeField] private int _maxTargetCount;
    [SerializeField] private int _killCountToAdd;
    
    [SerializeField] private Target _targetPrefab;

    public float MinRadius;
    public float MaxRadius;
    public float MinHeight;
    public float MaxHeight;

    private void Awake(){
        _targetCount = _startingTargetCount;
        for (int x = 0; x < _startingTargetCount; x++){
             SpawnNewTarget();
        }
    }

    private void OnEnable(){
        Target.OnDeath += SpawnNewTarget;
    }

    private void OnDisable(){
        Target.OnDeath -= SpawnNewTarget;
    }

    private void SpawnNewTarget(){
        var position = GenerateRandomPoint();
        GenerateTarget(position);

        _killCount++;
        if (_killCount % _killCountToAdd == 0 && _targetCount < _maxTargetCount){
            _targetCount++;
            position = GenerateRandomPoint();
            GenerateTarget(position);
        }
    }

    private Target GenerateTarget(Vector3 position){
        Target target = Instantiate(_targetPrefab, position, Quaternion.identity);
        target.transform.LookAt(Vector3.up);

        return target;
    }

    private Vector3 GenerateRandomPoint(){
        var random = Random.Range(0, 1f);

        var radius = Mathf.Lerp(MinRadius, MaxRadius, random);
        var theta = 2 * Mathf.PI * Random.Range(0f, 1);

        var x = radius * Mathf.Cos(theta);
        var z = radius * Mathf.Sin(theta);
        var y = Mathf.Lerp(MinHeight, MaxHeight, random);

        return new Vector3(x, y, z);
    }
}

