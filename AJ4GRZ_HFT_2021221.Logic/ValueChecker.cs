using AJ4GRZ_HFT_2021221.Models;
using AJ4GRZ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public class ValueChecker
    {
        public static void IsCorrect<T>(T entity)
        {
            var newEntity = entity;
            Type t = typeof(T);
            var props = t.GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(newEntity);
                bool isText = false;
                bool isId = false;
                bool isNumber = false;
                isText = prop.GetCustomAttribute<TextAttribute>() != null;
                isId = prop.GetCustomAttribute<IdAttribute>() != null;
                isNumber = prop.GetCustomAttribute<NumberAttribute>() != null;
                
                if ((value == null) && (isText && value.ToString() == string.Empty))
                {
                    throw new InvalidOperationException(prop.Name + " can't be empty!");
                }
                else if (isText && value.ToString().Length > 50)
                {
                    throw new InvalidOperationException(prop.Name + " can't be longer than 50 charaters!");
                }
                else if (isNumber && Convert.ToInt32(value) < 0)
                {
                    throw new InvalidOperationException(prop.Name + " can't be negative!");
                }
                else if (isNumber && !isId && Convert.ToInt32(value) == 0)
                {
                    throw new InvalidOperationException(prop.Name + " can't be 0!");
                }
                else if (isNumber)
                {
                    try
                    {
                        Convert.ToInt32(value);
                    }
                    catch (Exception)
                    {

                        throw new InvalidOperationException(prop.Name + "is supposed to be a number!");
                    }
                }
            }
        }

        public static void Exists<T>(T entity)
        {
            Type t = typeof(T);

            if (entity == null)
            {
                throw new InvalidOperationException(t.Name + " not found!");
            }
        }
    }
}
