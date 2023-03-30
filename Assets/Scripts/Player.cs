using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandSide{
    Left = 0,
    Right = 1
}

public class Player : MonoBehaviour{
    [SerializeField] private XRRayInteractor[] _rayInteractors;
    [SerializeField] private IUseable[] _useables = new IUseable[2];

    [SerializeField] private float _shotTriggerValue = 0.8f;
    [SerializeField] private float _triggerResetValue = 0.5f;
    
    private bool[] _hasShot = new bool[2];

    private void Awake(){
        foreach (var interactor in _rayInteractors){
            interactor.maxRaycastDistance = 100;
        }
    }

    public void OnLeftHandActivate(InputAction.CallbackContext context){
        float value = context.ReadValue<float>();
        var hand = (int)HandSide.Left;
        var weapon = _useables[hand];
        
        if (weapon == null)
            RayForFollowHand(hand);
        else{
            if (!_hasShot[hand] && value > _shotTriggerValue)
                weapon.Use();
            else if (_hasShot[hand] && value < _triggerResetValue)
                _hasShot[hand] = false;
        }
    }

    public void OnRightHandActivate(InputAction.CallbackContext context){
        float value = context.ReadValue<float>();
        var hand = (int)HandSide.Right;
        var weapon = _useables[hand];
        
        if (weapon == null)
            RayForFollowHand(hand);
        else{
            if (!_hasShot[hand] && value > _shotTriggerValue)
                weapon.Use();
            else if (_hasShot[hand] && value < _triggerResetValue)
                _hasShot[hand] = false;
        }
    }

    private void RayForFollowHand(int index){
        var interactor = _rayInteractors[index];
        
        interactor.TryGetCurrentRaycast(out var hit, out var hitIndex, out var uiHit, out var uiIndex, out var isUI); {
            if (isUI) return;
            if (!hit.HasValue) return;

            var interactable = hit.Value.collider.GetComponent<IInteractable>();
            if (interactable == null) return;

            interactable.Interact(this, index);
        }
    }

    public void SetUseable(IUseable useable, int index){
        if (_useables[index] == useable) return;
        if (_useables[index == 0 ? 1 : 0] == useable)
            _useables[index == 0 ? 1 : 0] = null;
        _useables[index] = useable;
    }

    public Transform GetHandTransform(int hand){
        return _rayInteractors[hand].transform;
    }
}