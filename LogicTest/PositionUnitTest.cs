﻿//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________
using Logic;

namespace LogicTest
{
  public class PositionUnitTest
  {
    [Test]
    public void ConstructorTestMethod()
    {
      Random random = new Random();
      double initialX = random.NextDouble();
      double initialY = random.NextDouble();
      IPosition position = new Position(initialX, initialY);
      Assert.AreEqual(initialX, position.x);
      Assert.AreEqual(initialY, position.y);
    }
  }
}