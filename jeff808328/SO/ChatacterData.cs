using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterData")]
public class ChatacterData : ScriptableObject
{
    public float MaxMoveSpeed; // ��t��
    public float AddSpeed; // �[�t��
    public float MinusSpeed; // ��t�� // �Ȭ���
    public float Gravity; // ���O
    public float JumpSpeed;
}
