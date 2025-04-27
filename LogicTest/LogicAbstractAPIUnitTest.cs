//____________________________________________________________________________________________________________________________________
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
  public class LogicAbstractAPIUnitTest
  {
	[Test]
	public void LogicConstructorTestMethod()
	{
		LogicAbstractAPI instance1 = LogicAbstractAPI.GetLogicLayer();
		LogicAbstractAPI instance2 = LogicAbstractAPI.GetLogicLayer();
		Assert.AreSame(instance1, instance2);
		instance1.Dispose();
		Assert.Throws<ObjectDisposedException>(() => instance2.Dispose());
	}
  }
}