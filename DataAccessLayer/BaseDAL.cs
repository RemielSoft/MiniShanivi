using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccessLayer
{
    public class BaseDAL
    {
        /////////////////////////************START**********//////////////////////////
        #region Value on basis of Data Type section
        /// <summary>
        /// Gets the date from reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected DateTime GetDateFromReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return reader.GetDateTime(reader.GetOrdinal(key));
            }
        }

        /// <summary>
        /// Gets the integer from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected Int32 GetIntegerFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return Int32.MinValue;
            }
            else
            {
                Int32 indexInt = reader.GetOrdinal(key);
                Object obj = reader.GetValue(indexInt);
                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
            }
            return Int32.MinValue;
        }

        /// <summary>
        /// Return date
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected String GetDate(DateTime dt)
        {
            return string.Format("{0:yyyy/MM/dd}", dt);
        }

        /// <summary>
        /// Return Current date
        /// </summary>
        protected String CurrentDate
        {
            get
            {
                return string.Format("{0:yyyy/MM/dd}", DateTime.Now.Date);
            }

        }

        /// <summary>
        /// Get Short From data reader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected Int16 GetShortFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return Int16.MinValue;
            }
            else
            {
                Int16 indexInt = Convert.ToInt16(reader.GetOrdinal(key));
                Object obj = reader.GetValue(indexInt);
                if (obj != null)
                {
                    return Convert.ToInt16(obj);
                }
            }
            return Int16.MinValue;
        }

        /// <summary>
        /// Gets the decimal from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected Decimal GetDecimalFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return Decimal.MinValue;
            }
            else
            {
                return reader.GetDecimal(reader.GetOrdinal(key));
            }
        }

        /// <summary>
        /// Gets the short integer from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected Int16 GetShortIntegerFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return Int16.MinValue;
            }
            else
            {
                Int32 indexInt = reader.GetOrdinal(key);
                Object obj = reader.GetValue(indexInt);
                if (obj != null)
                {
                    return Convert.ToInt16(obj);
                }
            }
            return Int16.MinValue;
        }

        /// <summary>
        /// Gets the long integer from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected Int64 GetLongIntegerFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return Int64.MinValue;
            }
            else
            {
                return reader.GetInt64(reader.GetOrdinal(key));
            }
        }

        /// <summary>
        /// Gets the string from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected String GetStringFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return null;
            }
            else
            {
                return reader.GetString(reader.GetOrdinal(key));
            }
        }

        /// <summary>
        /// Gets the boolean from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected bool GetBooleanFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return false;
            }
            else
            {
                Int16 booleanValue = reader.GetInt16(reader.GetOrdinal(key));
                if (booleanValue > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the byte from data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected byte[] GetByteFromDataReader(IDataReader reader, String key)
        {
            if (reader[key] == DBNull.Value)
            {
                return null;
            }
            else
            {
                return (byte[])reader[key];
            }
        }

        #endregion
        //////////////////////////************END************//////////////////////////


        /////////////////////////************START**********//////////////////////////
        #region Oup Put Parameter section

        /// <summary>
        /// Gets the integer from out put parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected Int32 GetIntegerFromOutPutParameter(Object value)
        {
            if (value == DBNull.Value)
            {
                return Int32.MinValue;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        /// <summary>
        /// Gets the boolean from out put parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected bool GetBooleanFromOutPutParameter(Object value)
        {
            if (value == DBNull.Value)
            {
                return false;
            }
            else
            {
                Int16 booleanValue = Convert.ToInt16(value);
                if (booleanValue > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the string from out put parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected String GetStringFromOutPutParameter(Object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToString(value);
            }
        }

        #endregion
        //////////////////////////************END************//////////////////////////
    }
}
