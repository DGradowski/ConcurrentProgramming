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

namespace ModelTest
{
  public class ModelAbstractAPITest
  {
    [Test]
    public void SingletonConstructorTestMethod()
    {
      ModelAbstractAPI instance1 = ModelAbstractAPI.CreateModel();
      ModelAbstractAPI instance2 = ModelAbstractAPI.CreateModel();
      Assert.AreSame(instance1, instance2);
    }
  }
}