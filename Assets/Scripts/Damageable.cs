﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    int Health { get; set; }
    void Damage();
}
