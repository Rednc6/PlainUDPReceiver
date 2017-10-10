using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib
{
    [Serializable]
    public class Car
    {
        public String Color { get; set; }
        public String Model { get; set; }
        public int RegNo { get; set; }

        public Car()
        {
            
        }

        public Car(String color, String model, int regNo)
        {
            this.Color = color;
            this.Model = model;
            this.RegNo = regNo;
        }
    }
}
