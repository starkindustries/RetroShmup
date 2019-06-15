using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Scorable
{
    int Points { get; set; }
    Scoreboard Scoreboard { get; set; }

    void Score();
}
