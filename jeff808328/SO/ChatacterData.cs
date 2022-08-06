using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterData")]
public class ChatacterData : ScriptableObject
{
    public float MaxMoveSpeed; // 初速度
    public float AddSpeed; // 加速度
    public float MinusSpeed; // 減速度 // 值為正
    public float Gravity; // 重力
    public float JumpSpeed;
}
