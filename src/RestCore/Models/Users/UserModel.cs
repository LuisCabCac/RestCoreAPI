using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCore.Models.Users
{
    public class UserModel
    {
        //How i usually use regions

        #region Attributes

        private int idField;
        private string nameField;
        private string birthDateField;

        #endregion Attributes

        #region Properties

        public int Id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }
        public string Name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }
        public string BirthDate
        {
            get { return this.birthDateField; }
            set { this.birthDateField = value; }
        }

        #endregion Properties

        #region Constructors

        public UserModel()
        {
        }
        public UserModel(string _name, string _birthDate)
        {
            nameField = _name;
            birthDateField = _birthDate;
            Random r = new Random();
            idField = r.Next(0, 100);
        }
        public UserModel(int _id, string _name, string _birthDate)
        {
            nameField = _name;
            birthDateField = _birthDate;
            idField = _id;
        }

        #endregion Constructors
    }
}
