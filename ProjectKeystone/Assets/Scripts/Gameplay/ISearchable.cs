using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface which implemnts fields necessary for simple search functionality.
/// </summary>
public interface ISearchable
{

    bool wasSearched
    {
        get;
        set;
    }

    List<ISearchable> children
    {
        get;
        set;
    }
}
