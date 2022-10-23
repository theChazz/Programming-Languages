using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stock_Broker_WebApp_1._00.Models
{
    public class JseIndicesEntity
    {
        private string mName;
        private double mRP;
        private double mMoveValue;
        private string mMovePercentage;

        public string Name { get => mName; set => mName = value; }
        public double RP { get => mRP; set => mRP = value; }
        public double MoveValue { get => mMoveValue; set => mMoveValue = value; }
        public string MovePercentage { get => mMovePercentage; set => mMovePercentage = value; }

    }
}