using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bandage : MonoBehaviour
{
    private int _sceneIndex;

    [SerializeField] private GameObject shadowingTransitionEffect;
    private EventManager eventManager;
    private Player player;

    private void Start()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        EventManager.instance.onLevelFinished += LevelFinished;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType() == typeof(CircleCollider2D))
        {
            //this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            
            this.gameObject.SetActive(false);
            LevelFinished();
            //EventManager.instance.LevelFinished();
        }
    }
    private void LevelFinished()
    {
        Debug.Log("_sceneIndex: " + _sceneIndex);
        _sceneIndex++;
        UnlockLevel();
        SceneManager.LoadScene(_sceneIndex);
        Debug.Log("_sceneIndex: " + _sceneIndex);

    }

    public void UnlockLevel()
    {
        if (_sceneIndex >= PlayerPrefs.GetInt("levels"))
            PlayerPrefs.SetInt("levels", _sceneIndex);
    }


    //IEnumerator ShadowingLevelTransition()
    //{
    //    //Instantiate(shadowingTransitionEffect, transform.position, Quaternion.identity);
    //    //yield return new WaitForSeconds(1);
    //    LevelFinished();
    //    UnlockLevel();
    //}

    private void OnDisable()
    {
        EventManager.instance.onLevelFinished -= LevelFinished;
    }
}
