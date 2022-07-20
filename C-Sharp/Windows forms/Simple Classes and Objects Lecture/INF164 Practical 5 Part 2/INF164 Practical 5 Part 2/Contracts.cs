using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF164_Practical_5_Part_2
{
    // STEP 2: Make class public
    public class Contracts
    {
            //Data attributes called data members
            private string mClientName;
            private string mClientSurname;
            private string mDeviceName;
            private DateTime mContractDate;
            private int mMonthsRemaining;


            //Optionally properties
            public string ClientName { get => mClientName; set => mClientName = value; }
            public string ClientSurname { get => mClientSurname; set => mClientSurname = value; }
            public string DeviceName { get => mDeviceName; set => mDeviceName = value; }
            public DateTime ContractDate { get => mContractDate; set => mContractDate = value; }
            public int MonthsRemaining { get => mMonthsRemaining; set => mMonthsRemaining = value; }

            //Optionally methods(that uses or processes the data attributes)
            public Contracts()
                {

                }

            public Contracts(string ClientName, string ClientSurname, string DeviceName, int MonthsRemaining, DateTime ContractDate)
            {
                mClientName = ClientName;
                mClientSurname = ClientSurname;
                mDeviceName = DeviceName;
                mMonthsRemaining = MonthsRemaining;
                mContractDate = ContractDate;
            }


            //Optionally events(mostly with interface classes, seldom with entity classes)
            public void ExtendContract()
            {
                mMonthsRemaining = mMonthsRemaining + 3;
            }

        }
    }
