using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void PrimaryAction();
    public abstract void SecondaryAction();
}
