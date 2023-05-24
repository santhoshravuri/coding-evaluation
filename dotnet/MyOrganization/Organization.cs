using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            // Generate a random ID for the new employee
            Random random = new Random();
            int id = random.Next();

            // Create a new employee and position
            Employee employee = new Employee(id, person);
            Position position = new Position(title, employee);

            // Find the first available position with the given title and hire  employee
            HireHelper(root, position);

            // Return the newly filled position or null if no position has that title
            return position.IsFilled() ? position : null;
        }

        private bool HireHelper(Position pos, Position newPosition)
        {
            if (pos.GetTitle() == newPosition.GetTitle() && !pos.IsFilled())
            {
                pos.SetEmployee(newPosition.GetEmployee());
                return true;
            }

            foreach (Position p in pos.GetDirectReports())
            {
                if (HireHelper(p, newPosition))
                    return true;
            }

            return false;
        }



        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
