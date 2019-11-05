using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Banks
{
    private static Dictionary<string, string> banks = new Dictionary<string, string>{
            { "Access Bank" , "044" },
            { "Citibank", "023" },
            { "Diamond Bank","063" },
            { "Dynamic Standard Bank","" },
            { "Ecobank Nigeria", "050" },
            { "Fidelity Bank Nigeria", "070" },
            { "First Bank of Nigeria", "011" },
            { "First City Monument Bank", "214" },
            { "Guaranty Trust Bank", "058" },
            { "Heritage Bank Plc", "030" },
            { "Jaiz Bank", "301" },
            { "Keystone Bank Limited", "082" },
            { "Providus Bank Plc", "101" },
            { "Polaris Bank", "076" },
            { "Stanbic IBTC Bank Nigeria Limited", "221" },
            { "Standard Chartered Bank", "068" },
            { "Sterling Bank", "232" },
            { "Suntrust Bank Nigeria Limited", "100" },
            { "Union Bank of Nigeria", "032" },
            { "United Bank for Africa", "033" },
            { "Unity Bank Plc", "215" },
            { "Wema Bank", "035" },
            { "Zenith Bank", "057" }
    };

    public static Dictionary<string, string> AllBanks()
    {
        return banks;
    }
}
