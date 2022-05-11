using System;

namespace AJ4GRZ_HFT_2021221.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TextAttribute : Attribute{}

    [AttributeUsage(AttributeTargets.Property)]
    public class NumberAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class IdAttribute : Attribute { }
}
