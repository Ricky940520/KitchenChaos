using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Has progress bar function
/// </summary>
public interface IHasProgressBar
{
    /// <summary>
    /// Update UI
    /// </summary>
    public Action<float> OnProgressBarChanged { get; set; }
}
