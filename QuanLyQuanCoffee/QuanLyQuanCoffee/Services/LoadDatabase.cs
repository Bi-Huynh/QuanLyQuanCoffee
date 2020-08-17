using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.Services
{
    class LoadDatabase
    {
        private static QuanLyQuanCoffeeEntities1 _instance;

        protected LoadDatabase()
        {

        }

        public static QuanLyQuanCoffeeEntities1 Instance()
        {
            if (_instance == null)
            {
                _instance = new QuanLyQuanCoffeeEntities1();
            }
            return _instance;
        }
    }
}
