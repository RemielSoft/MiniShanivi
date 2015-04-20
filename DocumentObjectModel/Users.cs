using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    [Serializable]
    public class Users:Base
    {
       
        
        #region Properties ...
        private int _User_Id;
        
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int UserId
        {
            get
            {
                if (_User_Id == int.MinValue)
                {
                    return 0;
                }
                return _User_Id;
            }
            set
            {
                _User_Id = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the user login id.
        /// </summary>
        /// <value>The user login id.</value>
        public string UserLoginId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                StringBuilder sbName = new StringBuilder();
                sbName.Append(FirstName);
                if (String.IsNullOrEmpty(MiddleName) == false)
                {
                    sbName.Append(' ' + MiddleName);

                }
                if (String.IsNullOrEmpty(LastName) == false)
                {
                    sbName.Append(' ' + LastName);

                }
                return sbName.ToString();
            }
        }

        public string EmpCode { get; set; }

        /// <summary>
        /// Gets or sets the OfficeExtensionNumber
        /// </summary>
        /// <value>The OfficeExtensionNumber.</value>
        public string OfficeExtensionNumber { get; set; }

        /// <summary>
        /// Gets or sets the MaritalStatus
        /// </summary>
        /// <value>The MaritalStatus.</value>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the Gender
        /// </summary>
        /// <value>The Gender.</value>
        public string Gender { get; set; }



        /// <summary>
        /// Gets or sets the full name with employee code.
        /// </summary>
        /// <value>The full name.</value>
        public string FullNameWithEmployeeCode
        {
            get
            {
                StringBuilder sbName = new StringBuilder();

                sbName.Append(FirstName);
                if (String.IsNullOrEmpty(MiddleName) == false)
                {
                    sbName.Append(' ' + MiddleName);

                }
                if (String.IsNullOrEmpty(LastName) == false)
                {
                    sbName.Append(' ' + LastName);

                }
                // sbName.Append(EmpCode);
                if (!String.IsNullOrEmpty(EmpCode) )
                {
                   //if (Convert.ToInt32(EmpCode) > 0)
                   // {
                   //     sbName.Append(" (" + EmpCode + " )");
                   // }
                    sbName.Append(" (" + EmpCode + " )");
                }

                return sbName.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }


        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the email id.
        /// </summary>
        /// <value>The email id.</value>
        public string EmailId { get; set; }

        public int UserGroupId { get; set; }
      

        /// <summary>
        /// Gets or sets the employee group detail.
        /// </summary>
        /// <value>The employee group detail.</value>
        public List<Group> Groups { get; set; }

        public String GroupName { get; set; } 
       
        /// <summary>
        /// Gets or sets the isdeleted.
        /// </summary>
        /// <value>The isdeleted.</value>
        public Int16 Isdeleted { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>The name of the department.</value>
        public Department Department { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the highest level group id.
        /// </summary>
        /// <returns></returns>
        public Group GetHighestLevelGroupId()
        {
            if (this.Groups != null && this.Groups.Count > 0)
            {
                //this.Groups.Sort();
                return this.Groups[0];
            }
            else
            {
                return null;
            }
        }

        public String Role()
        {

            //var result= from g in Groups where g.AuthorityLevel.Name==AuthorityLevelType.Admin.ToString();
            //var result1 = Groups.Where(g => g.AuthorityLevel.Name == AuthorityLevelType.Admin.ToString());


            if(this.Groups !=null && this.Groups.Count>0)
            {
                foreach (Group item in Groups)
                {
                    if (item.AuthorityLevel.Name == AuthorityLevelType.Admin.ToString())
                    {
                        return AuthorityLevelType.Admin.ToString();
                    }
                    else if (item.AuthorityLevel.Name == AuthorityLevelType.Purchase.ToString())
                    {
                        return AuthorityLevelType.Purchase.ToString();
                    }
                    else if (item.AuthorityLevel.Name == AuthorityLevelType.Project.ToString())
                    {
                        return AuthorityLevelType.Project.ToString();
                    }
                    else if (item.AuthorityLevel.Name == AuthorityLevelType.Finance.ToString())
                    {
                        return AuthorityLevelType.Finance.ToString();
                    }

                    else if (item.AuthorityLevel.Name == AuthorityLevelType.Store.ToString())
                    {
                        return AuthorityLevelType.Store.ToString();
                    }

                    else if (item.AuthorityLevel.Name == AuthorityLevelType.Executive.ToString())
                    {
                        return AuthorityLevelType.Executive.ToString();
                    }


                }
                


                return AuthorityLevelType.User.ToString();
            }
            return AuthorityLevelType.User.ToString();
        }

        #endregion
    }
}
