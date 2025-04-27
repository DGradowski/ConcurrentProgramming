//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________
using Data;

namespace DataTest
{
  public class VectorUnitTest
  {
    [Test]
    public void ConstructorTestMethod()
    {
      Random randomGenerator = new();
      double XComponent = randomGenerator.NextDouble();
      double YComponent = randomGenerator.NextDouble();
      Vector newInstance = new(XComponent, YComponent);
      Assert.AreEqual(XComponent, newInstance.x);
      Assert.AreEqual(YComponent, newInstance.y);
    }
  }
}