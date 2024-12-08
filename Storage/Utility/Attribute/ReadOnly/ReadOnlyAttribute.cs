using System;
using UnityEngine;

namespace PhantomEngine
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public readonly bool RuntimeOnly;

        public ReadOnlyAttribute(bool runtimeOnly = false)
        {
            RuntimeOnly = runtimeOnly;
        }
    }
}