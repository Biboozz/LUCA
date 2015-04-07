using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tree_builder.XMLClasses
{
    public class skill
    {
        public int ID;
        public int[] neighborsID;
        public string name;
        public string description;

        public actionData workCosts;
        public actionData workProducts;
        public actionData devCosts;
        public actionData devProducts;

        public bool innate;
        public string type;
        public int[] requiredID;

        public skill(int ID, int[] neighborsID, string name, string description, actionData workCosts, actionData workProducts, actionData devCosts, actionData devProducts, bool innate, string type, int[] requiredID)
        {
            this.ID = ID;
            this.neighborsID = neighborsID;
            this.description = description;
            this.workCosts = workCosts;
            this.workProducts = workProducts;
            this.devCosts = devCosts;
            this.devProducts = devProducts;
            this.innate = innate;
            this.type = type; this.requiredID = requiredID;
        }

        public skill(): this(-1, new int[] {-1,-1,-1,-1,-1,-1}, "unvalid name", "no description", new actionData(),new actionData(),new actionData(),new actionData(), false, "unvalid", new int[0])
        {

        }
    }
}
