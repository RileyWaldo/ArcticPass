using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeCabana.Utils;

namespace ArcticPass.Generators
{
    [CreateAssetMenu(fileName = "Names", menuName = "ArcticPass/NameList")]
    public class NameGenerator : RandomObjectGenerator<string>
    {
        
    }

    [CreateAssetMenu(fileName = "Names", menuName = "Audio")]
    public class NameTest : RandomObjectGenerator<AudioClip>
    {

    }
}

