using System.Collections;
using System.Collections.Generic;
using ECM2.Examples.SideScrolling;
using UnityEngine;

public class RotateAndChangeColor : MonoBehaviour
{
    // Rotation speed
    public float rotationSpeed = 100f;

    private void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManger的CanUseQ设为true
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.canUseQ = true;

                // 该物体消失
                Destroy(gameObject);
                
                // 播放sideScrollingCharacter的音效
                SideScrollingCharacter character = other.GetComponent<SideScrollingCharacter>();
                if (character != null)
                {
                    character.soundEffect.PlayOneShot(character.GetCoinClip);
                }
                
            }
        }
    }
}