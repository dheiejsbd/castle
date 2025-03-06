using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public event Action<HitBox, Collision2D> Enter;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enter?.Invoke(this, collision);
    }
}
