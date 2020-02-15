using System;
using System.Collections.Generic;

[Serializable]
public class StakeTypesResponse : IDeserilizable, IResponse
{
    public List<StakeTypeModel> stake_type { get; set; }
}

