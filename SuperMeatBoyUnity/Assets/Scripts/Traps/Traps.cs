using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    public abstract class Traps : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        //private Recorder recorder;
        public abstract void Killed();
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                KillPlayer();
                Killed();
            }
        }

        public void KillPlayer() => StartCoroutine(WaitForSpawn());

        IEnumerator WaitForSpawn()
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                ParticleSystem deathEffect = player.GetComponentInChildren<ParticleSystem>();
                if (deathEffect != null && deathEffect.name == "DeathEffect")
                    deathEffect.Play();

                CircleCollider2D playerCircleCollider = player.GetComponent<CircleCollider2D>();
                playerCircleCollider.enabled = false;

                BoxCollider2D playerBoxCollider = player.GetComponent<BoxCollider2D>();
                playerBoxCollider.enabled = false;

                Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
                playerRigidbody.bodyType = RigidbodyType2D.Static;

                Renderer playerRenderer = player.GetComponent<Renderer>();
                playerRenderer.enabled = false;

                player.GetComponent<ShadowCaster2D>().enabled = false;


                player.enabled = false;
                player.bloodSteps.SetActive(false);

                yield return new WaitForSeconds(1.5f);

                player.GetComponent<ShadowCaster2D>().enabled = true;
                player.bloodSteps.SetActive(true);
                SetBlocksOnSpawn();
                player.transform.position = player != null ? player.startPlayerPosition : Vector3.zero;

                playerBoxCollider.enabled = true;
                playerCircleCollider.enabled = true;
                player.enabled = true;

                playerRenderer.enabled = true;
                playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
            //EventManager.instance.SpawnPlayerOnStart();
            //r/*ecorder.StartNewRecording();*/
        }

        public void SetBlocksOnSpawn()
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("BreakingBlock");

            foreach (GameObject block in blocks)
            {
                BreakingBlock breakingBlock = block.GetComponent<BreakingBlock>();
                if (breakingBlock != null)
                    breakingBlock.SetStartBlockPosition();
            }
        }
        
    }
}
