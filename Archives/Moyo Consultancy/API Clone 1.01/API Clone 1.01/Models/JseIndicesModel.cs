using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Clone_1._00.Models
{
    public class JseIndices
    {
        private string mName;
        private double mRP;
        private double mMoveValue;
        private string mMovePercentage;

        public string Name { get => mName; set => mName = value; }
        public double RP { get => mRP; set => mRP = value; }
        public double MoveValue { get => mMoveValue; set => mMoveValue = value; }
        public string MovePercentage { get => mMovePercentage; set => mMovePercentage = value; }

        //public JseIndices()
        //{
        //    Name = "";
        //    RP = 0.00;
        //    MoveValue = 0.00;
        //    mMovePercentage = "0.00%";
        //}

        //public JseIndices(string name, double rp, double moveList, string movePercentage)
        //{
        //    Name = name;
        //    RP = rp;
        //    MoveValue = moveList;
        //    mMovePercentage = movePercentage;
        //}

    }
}
