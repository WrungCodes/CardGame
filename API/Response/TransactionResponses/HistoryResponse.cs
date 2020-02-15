using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryResponse : IDeserilizable, IResponse
{
    public List<HistroyModel> history { get; set; }
    
}
