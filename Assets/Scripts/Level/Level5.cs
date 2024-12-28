using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    public CharacterController characterController;

    public GameObject DieHint;
    public GameObject Hint1;


    void Start()
    {
        characterController = FindObjectOfType<CharacterController>();

        if (characterController != null)
        {
            characterController.onDie.AddListener(OnCharacterDie);
        }
    }

    void OnCharacterDie()
    {
        Debug.Log("Character has died. Event triggered.");

        // 开启脚本所在的物体
        DieHint.SetActive(true);
        Hint1.SetActive(false);
    }
}