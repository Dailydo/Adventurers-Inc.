﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour
{
    public Activities.Status _status;

    public void ResetSelf()
    {
        _status = Activities.Status.Available;
    }
}
