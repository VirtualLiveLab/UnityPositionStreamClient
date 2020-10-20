using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    [SerializeField] private TextMesh _name;

    public void Change(string name)
    {
        _name.text = name;
    }
}
