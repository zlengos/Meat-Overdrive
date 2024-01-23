using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{


    [SerializeField] private TileBase _bloodDown, _bloodLeft, _bloodRight, _bloodUpper;
    [SerializeField] private Tilemap UpDownTilemap, LeftRightTilemap;
    [SerializeField] public GameObject player;

    //[SerializeField] private UniversalRenderPipelineAsset urpAsset;
    [SerializeField] private Light2D[] lightComponents;
    private readonly float _offset = 0.72f;

    private void Start()
    {
        CheckGraphicsSettings();
        
    }

   
    private void LateUpdate()
    {
        SetBloodOnTile();

        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Alpha1))
            SceneManager.LoadScene(0);
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Alpha2))
            SceneManager.LoadScene(1);
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Alpha3))
            SceneManager.LoadScene(2);
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Alpha4))
            SceneManager.LoadScene(3);

        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    private void SetBloodOnTile()
    {
        Vector3 userPosition = player.transform.position;
        Vector3 LowerUserPosition = new Vector3(userPosition.x, userPosition.y - _offset, userPosition.z);
        Vector3Int cellLowerPosition = UpDownTilemap.WorldToCell(LowerUserPosition);
        TileBase tileLower = UpDownTilemap.GetTile(cellLowerPosition);


        Vector3 UpperUserPosition = new Vector3(userPosition.x, userPosition.y + _offset, userPosition.z);
        Vector3Int cellUpperPosition = UpDownTilemap.WorldToCell(UpperUserPosition);
        TileBase tileUpper = UpDownTilemap.GetTile(cellUpperPosition);

        Vector3 LeftUserPosition = new Vector3(userPosition.x - _offset, userPosition.y, userPosition.z);
        Vector3Int cellLeftPosition = LeftRightTilemap.WorldToCell(LeftUserPosition);
        TileBase tileLeft = LeftRightTilemap.GetTile(cellLeftPosition);

        Vector3 RightUserPosition = new Vector3(userPosition.x + _offset, userPosition.y, userPosition.z);
        Vector3Int cellRightPosition = LeftRightTilemap.WorldToCell(RightUserPosition);
        TileBase tileRight = LeftRightTilemap.GetTile(cellRightPosition);


        if (tileLower != null)
            UpDownTilemap.SetTile(cellLowerPosition, _bloodDown);
        if (tileUpper != null)
            UpDownTilemap.SetTile(cellUpperPosition, _bloodUpper);
        if (tileLeft != null)
            LeftRightTilemap.SetTile(cellLeftPosition, _bloodLeft);
        if (tileRight != null)
            LeftRightTilemap.SetTile(cellRightPosition, _bloodRight);
    }

    private void CheckGraphicsSettings()
    {
        if (QualitySettings.GetQualityLevel() >= 2)
        {
            foreach (var light in lightComponents)
            {
                if (!light.enabled)
                    light.enabled = true;
            }
            
        }
        else
        {
            foreach (var light in lightComponents)
            {
                if (light.enabled)
                    light.enabled = false;
            }
            
        }
    }
}
