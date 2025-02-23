using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    [Test]
    public void PlayerTestSimplePasses()
    {
        Debug.Log("Test");
    }
    
    [UnityTest]
    public IEnumerator PlayerTestWithEnumeratorPasses()
    {
        Debug.Log("UnityTest");
        yield return null;
    }
}
