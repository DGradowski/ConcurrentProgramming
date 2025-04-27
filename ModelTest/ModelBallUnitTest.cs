//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

using Model;
using Logic;

namespace ModelTest
{
  public class ModelBallUnitTest
  {
    [Test]
    public void ConstructorTestMethod()
    {
      Model.Ball ball = new Model.Ball(0.0, 0.0, new BusinessLogicIBallFixture());
      Assert.AreEqual(0.0, ball.Top);
      Assert.AreEqual(0.0, ball.Top);
    }

    [Test]
    public void PositionChangeNotificationTestMethod()
    {
      int notificationCounter = 0;
      Model.Ball ball = new Model.Ball(0, 0.0, new BusinessLogicIBallFixture());
      ball.PropertyChanged += (sender, args) => notificationCounter++;
      Assert.AreEqual(0, notificationCounter);
      ball.Left = 1.0;
      Assert.AreEqual(1, notificationCounter);
      Assert.AreEqual(1.0, ball.Left);
      Assert.AreEqual(0.0, ball.Top);
      ball.Top = 1.0;
      Assert.AreEqual(2, notificationCounter);
      Assert.AreEqual(1.0, ball.Left);
      Assert.AreEqual(1.0, ball.Top);
    }

    private class BusinessLogicIBallFixture : Logic.IBall
    {
      public event EventHandler<IPosition>? NewPositionNotification;

      public void Dispose()
      {
        throw new NotImplementedException();
      }
    }
  }
}