using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyQCodingRobot.Robots;

namespace MyQCodingRobotTests.Robots
{
	[TestClass()]
	public class RobotStrategyTests
	{
		[TestMethod()]
		public void SetByResultTest_SetNext()
		{
			// arrange
			RobotStrategy robotStrategy = new RobotStrategyFixture().AddClean().SetTurnRightToBackOff().Generate();

			// act
			robotStrategy.SetByResult(true);

			// assert
			string? code = robotStrategy.GetNextStep()?.Code;
			Assert.AreEqual(RobotMoveConfiguration.Clean.Code, code);
		}

		[TestMethod()]
		public void SetByResultTest_SwitchToBackOff()
		{
			// arrange
			RobotStrategy robotStrategy = new RobotStrategyFixture().AddTurnLeft().SetTurnRightToBackOff().Generate();

			// act
			robotStrategy.SetByResult(false);

			// assert
			string? code = robotStrategy.GetNextStep()?.Code;
			Assert.AreEqual(RobotMoveConfiguration.TurnRight.Code, code);
		}

		[TestMethod()]
		public void GetNextStepTest_Solve()
		{
			// arrange
			RobotStrategy robotStrategy = new RobotStrategyFixture().AddClean().AddClean().SetTurnRightToBackOff().Generate();

			// act
			RobotMoveConfiguration? step1 = robotStrategy.GetNextStep();
			RobotMoveConfiguration? step2 = robotStrategy.GetNextStep();
			RobotMoveConfiguration? step3 = robotStrategy.GetNextStep();

			// assert
			Assert.AreEqual(RobotMoveConfiguration.Clean.Code, step1?.Code);
			Assert.AreEqual(RobotMoveConfiguration.Clean.Code, step2?.Code);
			Assert.IsNull(step3);
		}

		[TestMethod()]
		public void GetNextStepTest_ContinueAfterBackOff()
		{
			// arrange
			RobotStrategy robotStrategy = new RobotStrategyFixture().AddClean().AddClean().SetTurnRightToBackOff().Generate();

			// act
			RobotMoveConfiguration? step1 = robotStrategy.GetNextStep();
			robotStrategy.SetByResult(false);
			RobotMoveConfiguration? bstep1 = robotStrategy.GetNextStep();
			RobotMoveConfiguration? bstep2 = robotStrategy.GetNextStep();
			RobotMoveConfiguration? step2 = robotStrategy.GetNextStep();
			RobotMoveConfiguration? step3 = robotStrategy.GetNextStep();

			// assert
			Assert.AreEqual(RobotMoveConfiguration.Clean.Code, step1?.Code);
			Assert.AreEqual(RobotMoveConfiguration.Clean.Code, step2?.Code);
			Assert.AreEqual(RobotMoveConfiguration.TurnRight.Code, bstep1?.Code);
			Assert.AreEqual(RobotMoveConfiguration.TurnRight.Code, bstep2?.Code);
			Assert.IsNull(step3);
		}

		[TestMethod()]
		public void GetNextStepTest_FinishBackOff()
		{
			// arrange
			RobotStrategy robotStrategy = new RobotStrategyFixture().AddClean().AddClean().SetTurnRightToBackOff().Generate();

			// act
			RobotMoveConfiguration? step1 = robotStrategy.GetNextStep();
			robotStrategy.SetByResult(false);
			RobotMoveConfiguration? bstep1 = robotStrategy.GetNextStep();
			bool result = robotStrategy.SetByResult(false);

			// assert
			Assert.AreEqual(RobotMoveConfiguration.Clean.Code, step1?.Code);
			Assert.AreEqual(RobotMoveConfiguration.TurnRight.Code, bstep1?.Code);
			Assert.IsFalse(result);
		}

		[TestMethod()]
		public void GetNextStepTest_SwitchToNextBackOff()
		{
			// arrange
			RobotStrategy robotStrategy = new RobotStrategyFixture().AddClean().AddClean().SetDoubleBackOff().Generate();

			// act
			RobotMoveConfiguration? step1 = robotStrategy.GetNextStep();
			robotStrategy.SetByResult(false);
			RobotMoveConfiguration? bstep1 = robotStrategy.GetNextStep();
			robotStrategy.SetByResult(false);
			RobotMoveConfiguration? bstep2 = robotStrategy.GetNextStep();
			RobotMoveConfiguration? bstep3 = robotStrategy.GetNextStep();


			// assert
			Assert.AreEqual(RobotMoveConfiguration.Clean.Code, step1?.Code);
			Assert.AreEqual(RobotMoveConfiguration.TurnRight.Code, bstep1?.Code);
			Assert.AreEqual(RobotMoveConfiguration.TurnLeft.Code, bstep2?.Code);
			Assert.AreEqual(RobotMoveConfiguration.TurnLeft.Code, bstep3?.Code);
		}

		private class RobotStrategyFixture
		{
			public RobotStrategy? RobotStrategy { get; set; }

			private IList<RobotMoveConfiguration> Configuration { get; set; } = new List<RobotMoveConfiguration>();

			private string[][]? BackOffStrategy { get; set; }

			public RobotStrategyFixture SetTurnRightToBackOff()
			{
				BackOffStrategy = new string[][] {
					new string[] { "TR", "TR" }
				};
				return this;
			}

			public RobotStrategyFixture SetDoubleBackOff()
			{
				BackOffStrategy = new string[][] {
					new string[] { "TR", "TR" },
					new string[] { "TL", "TL" }
				};
				return this;
			}

			public RobotStrategyFixture AddClean()
			{
				Configuration.Add(RobotMoveConfiguration.Clean);
				return this;
			}

			public RobotStrategyFixture AddTurnLeft()
			{
				Configuration.Add(RobotMoveConfiguration.TurnLeft);
				return this;
			}

			public RobotStrategy Generate()
			{
				RobotStrategy = new RobotStrategy(Configuration, BackOffStrategy);
				return RobotStrategy;
			}

		}
	}
}
