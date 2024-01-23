using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCameraOnPlayer : MonoBehaviour
{
    private Transform _player;
    
    [SerializeField] private float _leftLimitCamera, _rightLimitCamera, _upperLimitCamera, _bottomLimitCamera;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.x = _player.transform.position.x;
        temp.y = _player.transform.position.y;  

        transform.position = temp;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, _leftLimitCamera, _rightLimitCamera), //x
            Mathf.Clamp(transform.position.y, _bottomLimitCamera, _upperLimitCamera), //y
            transform.position.z);

        
    }


}
