﻿using System.Collections.Generic;
using UnityEngine;

namespace NormandErwan.MasterThesisExperiment.UI.Grid
{
  public interface IGrid<T, U> : IGridElement<T>
    where T : IGridElement<T>
    where U : IGridElement<U>
  {
    // Properties

    Vector2Int GridSize { get; set; }
    Vector2 ElementScale { get; set; }
    Vector2 ElementMargin { get; set; }
    U ElementPrefab { get; set; }

    IEnumerable<U> Elements { get; }
    bool IsFull { get; }

    // Methods

    void BuildGrid();
    void CleanGrid();
    void AddElement(U cell);
    void RemoveElement(U cell);
  }
}