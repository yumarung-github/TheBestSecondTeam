using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/Skill", order = 0)]
public class Skill : ScriptableObject
{
    [SerializeField]
    private string skillName;
    public string SkillName
    {
        get { return skillName; }
    }
    [SerializeField]
    private string skillInfo;
    public string SkillInfo
    {
        get { return skillInfo; }
    }
}
