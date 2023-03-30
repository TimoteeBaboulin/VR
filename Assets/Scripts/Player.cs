using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandSide{
    Left = 0,
    Right = 1
}

public class Player : MonoBehaviour{
    [SerializeField] private XRRayInteractor[] _rayInteractors;
    [SerializeField] private IUseable[] _useables = new IUseable[2];

    private void Awake(){
        foreach (var interactor in _rayInteractors){
            interactor.maxRaycastDistance = 100;
        }
    }

    public void OnLeftHandActivate(){
        var hand = (int)HandSide.Left;
        var weapon = _useables[hand];
        
        if (weapon == null)
            RayForFollowHand(hand);
        else
            weapon.Use();
    }

    public void OnRightHandActivate(){
        var hand = (int)HandSide.Right;
        var weapon = _useables[hand];
        
        if (weapon == null)
            RayForFollowHand(hand);
        else
            weapon.Use();
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