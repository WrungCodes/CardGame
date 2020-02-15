using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanksResponse : IDeserilizable, IResponse
{
    public List<BankModel> banks { get; set; }
}
