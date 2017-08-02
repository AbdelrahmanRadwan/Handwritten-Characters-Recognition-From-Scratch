using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace PatternTask3
{
    class AllClasses
    {
        public ArrayList All = new ArrayList();
        public ArrayList Zerocollection = new ArrayList();
        public ArrayList Onecollection = new ArrayList();
        public ArrayList Towcollection = new ArrayList();
        public ArrayList Threecollection = new ArrayList();
        public ArrayList Fourcollection = new ArrayList();
        public ArrayList Fivecollection = new ArrayList();
        public ArrayList Sixcollection = new ArrayList();
        public ArrayList Sevencollection = new ArrayList();
        public ArrayList Eightcollection = new ArrayList();
        public ArrayList Ninecollection = new ArrayList();


         

        public void FillClasses(ArrayList AllData,int NumOfData)
        {
            for (int i = 0; i < NumOfData; i++)
            {
                if (((DigitalImage)AllData[i]).label ==(byte) 0)
                {
                    Zerocollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)1)
                {
                    Onecollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)2)
                {
                    Towcollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)3)
                {
                    Threecollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)4)
                {
                    Fourcollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)5)
                {
                    Fivecollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)6)
                {
                    Sixcollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)7)
                {
                    Sevencollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)8)
                {
                    Eightcollection.Add(AllData[i]);
                }
                else if (((DigitalImage)AllData[i]).label == (byte)9)
                {
                    Ninecollection.Add(AllData[i]);
                }
                
            }
            All.Add(Zerocollection); All.Add(Onecollection);
            All.Add(Towcollection); All.Add(Threecollection);
            All.Add(Fourcollection); All.Add(Fivecollection);
            All.Add(Sixcollection); All.Add(Sevencollection);
            All.Add(Eightcollection); All.Add(Ninecollection); 
        }


    }
}
