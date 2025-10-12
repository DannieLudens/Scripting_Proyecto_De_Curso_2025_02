using NUnit.Framework;
using UnityEngine;

public class CollectibleEditModeTests
{
    private CollectibleManager manager;

    [SetUp]
    public void Setup()
    {
        var managerObj = new GameObject("CollectibleManager");
        manager = managerObj.AddComponent<CollectibleManager>();

        manager.AddCollectible("Money", 0);
        manager.AddCollectible("Card", 0);

    }
    
    [TestCase(3)]
    [TestCase(2)]
    [TestCase(0)]
    [TestCase(-1)]
    public void AddMoney_Correct(int money)
    {
        manager.AddCollectible("Money", money);
        Assert.That(manager.GetCollectibleCount("Money"), Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void AddCard_IncreasesCount()
    {
        manager.AddCollectible("Card", 1);
        Assert.AreEqual(1, manager.GetCollectibleCount("Card"));
    }

    [Test]
    public void SpendCard_DecreasesCount()
    {
        manager.AddCollectible("Card", 2);
        manager.WithdrawCollectible("Card", 1);

        Assert.AreEqual(1, manager.GetCollectibleCount("Card"));
    }
}
