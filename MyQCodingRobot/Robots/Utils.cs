using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQCodingRobot.Robots
{
    public static class Utils
    {
        public static bool IsNotOk(this RobotOperationResult robotOperationResult)
        {
            return robotOperationResult != RobotOperationResult.Ok;
        }
    }
}
