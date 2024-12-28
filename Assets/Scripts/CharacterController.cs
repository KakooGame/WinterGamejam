using System;
using System.Collections;
using System.Collections.Generic;
using ECM2.Examples.SideScrolling;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public enum MaterialColor
{
    White,
    Grey,
    Black,
    LightYellow,
    MiddleYellow,
    DarkYellow,
    LightPink,
    MiddlePink,
    DarkPink,
    LightGreen,
    MiddleGreen,
    DarkGreen,
    LightBlue,
    MiddleBlue,
    DarkBlue,
}

public class CharacterController : MonoBehaviour
{
    // 材质编号
    public MaterialColor materialColor;


    // 新的材质
    public Material[] newMaterials;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    // 爆炸特效
    public GameObject explosionPrefab;

    // 是否是黑白模式
    public bool isBlackAndWhite = false;


    // 提示可以按下Ctrl的UI
    public GameObject crouchHint;

    public UnityEvent onDie;

    public AudioSource successSound;

    public GameObject UICanvasGameObject;


    public void Awake()
    {
        DontDestroyOnLoad(this);


        // Initialize the Unity event
        if (onDie == null)
        {
            onDie = new UnityEvent();
        }
    }

    private void Update()
    {
        // 如果transform的position.y小于-20，则死亡
        if (transform.position.y < -20)
        {
            Die();
        }
    }

    // 新的切换材质的方法
    [Button]
    public void ChangeRendererMaterialNew()
    {
    }

    // 切换材质的方法
    [Button]
    public void ChangeRendererMaterial()
    {
        if (skinnedMeshRenderer != null)
        {
            // 获取当前的材质数组
            Material[] materials = skinnedMeshRenderer.materials;

            // 替换第一个材质（或其他索引的材质）
            if (materials.Length > 0)
            {
                materials[0] = newMaterials[(int)materialColor];
            }

            // 将修改后的材质数组赋值回去
            skinnedMeshRenderer.materials = materials;
        }
        else
        {
            Debug.LogError("未找到 SkinnedMeshRenderer 组件！");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // 使用switch语句，根据tag切换材质
        switch (other.gameObject.tag)
        {
            case "White":
                materialColor = MaterialColor.White;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.White];
                break;
            case "Grey":
                materialColor = MaterialColor.Grey;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.Grey];
                break;
            case "Black":
                materialColor = MaterialColor.Black;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.Black];
                break;
            case "LightYellow":
                materialColor = MaterialColor.LightYellow;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.LightYellow];
                break;
            case "MiddleYellow":
                materialColor = MaterialColor.MiddleYellow;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.MiddleYellow];
                break;
            case "DarkYellow":
                materialColor = MaterialColor.DarkYellow;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.DarkYellow];
                break;
            case "LightPink":
                materialColor = MaterialColor.LightPink;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.LightPink];
                break;
            case "MiddlePink":
                materialColor = MaterialColor.MiddlePink;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.MiddlePink];
                break;
            case "DarkPink":
                materialColor = MaterialColor.DarkPink;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.DarkPink];
                break;
            case "LightGreen":
                materialColor = MaterialColor.LightGreen;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.LightGreen];
                break;
            case "MiddleGreen":
                materialColor = MaterialColor.MiddleGreen;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.MiddleGreen];
                break;
            case "DarkGreen":
                materialColor = MaterialColor.DarkGreen;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.DarkGreen];
                break;
            case "LightBlue":
                materialColor = MaterialColor.LightBlue;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.LightBlue];
                break;
            case "MiddleBlue":
                materialColor = MaterialColor.MiddleBlue;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.MiddleBlue];
                break;
            case "DarkBlue":
                materialColor = MaterialColor.DarkBlue;
                skinnedMeshRenderer.material = newMaterials[(int)MaterialColor.DarkBlue];
                break;
            default:
                Debug.LogWarning("Unknown tag: " + other.gameObject.tag);
                break;
        }

        // 当该物体图层为"Floor"时，执行方法
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("碰到了地板！");

            // 得到该物体上的Floor脚本
            Floor floor = other.GetComponent<Floor>();

            // 如果不是黑白模式
            if (FindObjectOfType<AdjustSaturation>().saturationValue != -100)
            {
                // 如果玩家的颜色和地板的颜色不一样，暂停游戏
                if (floor != null && floor.currentState == Floor.State.Colored && floor.currentColor != materialColor)
                {
                    Die();
                }
            }
            else
            {
                // 如果是黑白模式，根据玩家角色的颜色系列允许其站在相应的地板上
                if (floor != null)
                {
                    bool canStand = false;

                    switch (materialColor)
                    {
                        case MaterialColor.LightYellow:
                        case MaterialColor.LightPink:
                        case MaterialColor.LightGreen:
                        case MaterialColor.LightBlue:
                            canStand = (floor.currentBlackGreyWhite == Floor.BlackGreyWhite.White);
                            break;

                        case MaterialColor.MiddleYellow:
                        case MaterialColor.MiddlePink:
                        case MaterialColor.MiddleGreen:
                        case MaterialColor.MiddleBlue:
                            canStand = (floor.currentBlackGreyWhite == Floor.BlackGreyWhite.Grey);
                            break;

                        case MaterialColor.DarkYellow:
                        case MaterialColor.DarkPink:
                        case MaterialColor.DarkGreen:
                        case MaterialColor.DarkBlue:
                            canStand = (floor.currentBlackGreyWhite == Floor.BlackGreyWhite.Black);
                            break;
                    }

                    if (!canStand)
                    {
                        Die();
                    }
                }
            }
        }

        // 当玩家碰到切换场景的门时，切换到当前场景编号+1的场景
        if (other.gameObject.tag == "SwitchScene")
        {
            FindObjectOfType<GameManager>()
                .ChangeScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);

            // 重置角色状态
            ResetCharacter();
        }

        // 如果tag是CanUseCtrl，则显示提示UI
        if (other.gameObject.tag == "CanUseCtrl")
        {
            crouchHint.SetActive(true);
            SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
            character.canCrouch = true;
        }

        if (other.gameObject.tag == "CanUseCtrlAndBeWhite")
        {
            // 如果是黑白模式
            if (FindObjectOfType<AdjustSaturation>().saturationValue == -100)
            {
                crouchHint.SetActive(true);
                SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
                character.canCrouch = true;
            }
        }

        // 如果tag时Success，播放胜利音效，5s后切换到场景0.
        if (other.gameObject.tag == "Success")
        {
            SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
            // character.soundEffect.PlayOneShot(character.Success1);

            successSound.Play();

            // 找到场景中名为UICanvas的物体，开启
            // GameObject.Find("UICanvas").SetActive(true);
            if (UICanvasGameObject != null)
            {
                UICanvasGameObject.SetActive(true);
            }

            // Time.timeScale = 0;
            // Invoke("ResetCharacter", 5f);

            // 6s后调用GameManager的ChangeScene方法，切换到场景0
            Invoke("ChangeToStartScene", 2f);
        }
    }

    private void ChangeToStartScene()
    {
        FindObjectOfType<GameManager>().ChangeScene(0);
    }

    public void OnTriggerStay(Collider other)
    {
        // 当该物体图层为"Floor"时，执行方法
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("碰到了地板！");

            // 得到该物体上的Floor脚本
            Floor floor = other.GetComponent<Floor>();

            // 如果不是黑白模式
            if (FindObjectOfType<AdjustSaturation>().saturationValue != -100)
            {
                // 如果玩家的颜色和地板的颜色不一样，暂停游戏
                if (floor != null && floor.currentState == Floor.State.Colored && floor.currentColor != materialColor)
                {
                    Die();
                }
            }
            else
            {
                // 如果是黑白模式，根据玩家角色的颜色系列允许其站在相应的地板上
                if (floor != null)
                {
                    bool canStand = false;

                    switch (materialColor)
                    {
                        case MaterialColor.LightYellow:
                        case MaterialColor.LightPink:
                        case MaterialColor.LightGreen:
                        case MaterialColor.LightBlue:
                            canStand = (floor.currentBlackGreyWhite == Floor.BlackGreyWhite.White);
                            break;

                        case MaterialColor.MiddleYellow:
                        case MaterialColor.MiddlePink:
                        case MaterialColor.MiddleGreen:
                        case MaterialColor.MiddleBlue:
                            canStand = (floor.currentBlackGreyWhite == Floor.BlackGreyWhite.Grey);
                            break;

                        case MaterialColor.DarkYellow:
                        case MaterialColor.DarkPink:
                        case MaterialColor.DarkGreen:
                        case MaterialColor.DarkBlue:
                            canStand = (floor.currentBlackGreyWhite == Floor.BlackGreyWhite.Black);
                            break;
                    }

                    if (!canStand)
                    {
                        Die();
                    }
                }
            }
        }

        if (other.gameObject.tag == "CanUseCtrlAndBeWhite")
        {
            // 如果是黑白模式
            if (FindObjectOfType<AdjustSaturation>().saturationValue == -100)
            {
                crouchHint.SetActive(true);
                SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
                character.canCrouch = true;
            }
            else
            {
                crouchHint.SetActive(false);
                SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
                character.canCrouch = false;
            }
        }

        if (other.gameObject.tag == "CanUseCtrlBeColor")
        {
            // 如果是彩色模式
            if (FindObjectOfType<AdjustSaturation>().saturationValue != -100)
            {
                crouchHint.SetActive(true);
                SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
                character.canCrouch = true;
            }
            else
            {
                crouchHint.SetActive(false);
                SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
                character.canCrouch = false;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // 如果tag是CanUseCtrl，则关闭canCrouch
        if (other.gameObject.tag == "CanUseCtrl")
        {
            SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
            character.canCrouch = false;
            crouchHint.SetActive(false);

            // 角色站起来
            character.UnCrouch();
        }

        if (other.gameObject.tag == "CanUseCtrlAndBeWhite")
        {
            SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
            character.canCrouch = false;
            crouchHint.SetActive(false);

            // 角色站起来
            character.UnCrouch();
        }

        if (other.gameObject.tag == "CanUseCtrlBeColor")
        {
            SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
            character.canCrouch = false;
            crouchHint.SetActive(false);

            // 角色站起来
            character.UnCrouch();
        }
    }

    // 玩家死亡执行办法
    public void Die()
    {
        // 禁止角色控制
        SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();

        // 播放sideScrollingCharacter中的死亡音效
        character.soundEffect.PlayOneShot(character.ExplosionClip);


        // Invoke the Unity event
        onDie.Invoke();

        Debug.Log("玩家死亡！");

        // 开启爆炸特效
        explosionPrefab.SetActive(true);


        character.canControl = false;

        // 关闭角色的碰撞体
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        //角色消失
        skinnedMeshRenderer.enabled = false;


        // 2秒后重置角色状态
        Invoke("ResetCharacter", 2.1f);
        // 2秒后重新加载当前场景
        Invoke("ReloadScene", 2f);
    }

    // 切换场景时，重置角色状态
    public void ResetCharacter()
    {
        SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
        character.canControl = true;

        // 关闭角色的碰撞体
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        skinnedMeshRenderer.enabled = true;

        // 关闭爆炸特效
        explosionPrefab.SetActive(false);

        materialColor = MaterialColor.LightYellow;
        ChangeRendererMaterial();

        Time.timeScale = 1;
    }

    // 重新加载当前场景
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()
            .buildIndex);
    }
}