using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniERP.Shared
{
    public class ValidationExpression
    {
        public const String C_ALPHANUMERIC = @"^[^;>;&;<;%?*!~'`;!@#$\/*:,."";+=|]*$";

        // public const String C_ADDRESS = @"^[^;>;&;<;%?$@{}*!~'`;:""+=|]{1,150}$";

        public const String C_COMPANY_NAME = @"^[a-zA-Z0-9._\s]{1,50}$";   //Added by Vinay

        //public const String C_ADDRESS = @"^[a-zA-Z0-9.!$`~_;\:\/\/'()@#+-=*\&%|?^\s]{0,2000}$";  //Added by Jai

        public const String C_DESCRIPTION = @"^[\s\S]{0,250}$";//Manveer
        public const String C_ADDRESS = @"^([a-zA-Z0-9,\./\?;':""[\]\\{}\|`~!@#\$%\^&\*()-_=\+]*)$";  //Added by Jai

        //public const string C_New = @"^[\s\S]{0,10}$";

        public const String C_USER_ID = @"^[a-zA-Z0-9._]{1,50}$";

        public const String C_ALPHABETS = @"^[a-zA-Z]{1,100}$";

        public const String C_ALPHABETS_NAMES = @"^[a-zA-Z\s]{1,200}$";  //Added by Jai for space in b/w name

        public const String C_MOBILE_NUMBER = @"^[+0-9\s,-]{1,200}$";

        public const String C_NUMERIC = @"^[0-9]{1,18}$";

        public const String C_NUMERIC_DISCOUNT = @"^(\.[0-9]{0,2})|(([0-9]{1,2})\.?[0-9]{0,2})$";   //Added by Jai for discount not more than 100 on 8-02-13

        //-- public const String C_NUMERIC_DISCOUNT = @"^[0-9]{1,2}(?:\.[0-9]{1,2})?$";   //Added by Jai for discount not more than 100

        public const String C_DECIMAL = @"^(\.[0-9]{0,3})|(([0-9]{1,15})\.?[0-9]{0,3})$";

        public const string C_DECIMAL_7_2 = @"^\d{0,7}(\.\d{1,2})?$";

        public const String C_EMAIL_ID = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public const String C_DATE = @"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$";

        public const string C_TEXT_BOX_MULTILINE = @"^[^>;&;<;%?#$@#(){}*!~'`;:"";+=|\/]{1,150}$";

        public const String C_TIME = @"((1+[012]+)|([123456789]{1}))(((:|\s)[0-5]+[0-9]+))?(\s)?((a|A|p|P)(m|M))";

        public const String C_DATE_FORMAT = @"dd/MM/yyyy";

        public const String C_PIN_CODE = @"^[0-9]{6,10}$"; //Added by Jai for min 6 digit pin code

        public const String C_MOBILE_PHONE_NUMBER = @"^[+0-9\s,-]{10,15}$";  //Added by Jai for min 10 digit phone number

        public const String C_PAN_NUMBER = @"[A-Z]{5}\d{4}[A-Z]{1}$";   //Added By Vinay




        /// <summary>
        /// Validates a name. Allows up to 40 uppercase and lowercase characters and a few special characters that are common to some names. You can modify this list.
        /// </summary>Manveer
        public const String NAME = @"^[a-zA-Z''-'\s]{1,40}$";

        /// <summary>
        /// Validates a URL
        /// </summary>Manveer 
        public const String WEB_URL = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";

        //public const String WEB_URL = @"^\(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";

        /// <summary>
        /// Validates a strong password. It must be between 8 and 10 characters, contain at least one digit and one alphabetic character, and must not contain special characters.
        /// </summary>Manveer
        public const String C_PASSWORD = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{5,20})$";

        /// <summary>
        /// Validates a strong password. It must be minimum 8 characters, contain at least one digit and one alphabetic character, and one special characters.
        /// </summary>
        public const String C_PASSWORD_WITH_SPECIAL = @"(?=^.{8,}$)(|(?=.*\W+))(?![.\n])(?=.*[0-9a-zA-Z]).*$"; //Added by Vinay
    }
}