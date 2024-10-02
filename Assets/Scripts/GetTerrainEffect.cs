using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTerrainEffect : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    float defaultAngularDrag;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        defaultAngularDrag = _playerMovement.rb.angularDrag;
    }
    
    public void ModifyAngularDrag(float newAngularDrag)
    {
        _playerMovement.rb.angularDrag = newAngularDrag;
    }

    public void ResetAngularDrag()
    {
        _playerMovement.rb.angularDrag = defaultAngularDrag;
    }
}
