using UnityEngine;

namespace PhantomEngine
{
    public class VariableAttribute : PropertyAttribute
    {
        public string variable { get; private set; }

        public VariableAttribute(string name)
        {
            variable = name;
        }
    }
}