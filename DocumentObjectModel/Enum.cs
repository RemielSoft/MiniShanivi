using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    public enum AuthorityLevelType
    {
        Admin = 1,
        Purchase = 2,
        User = 3,
        Project = 4,
        Finance = 5,
        Store  =6,
        Executive = 7,
    }
    public enum StatusType
    {
        Pending = 1,
        Rejected = 2,
        Approved = 3,
        Generated = 4,
        InComplete = 5,
        Close = 10,
        
    }

    public enum DiscountType
    {
        None = 0,
        Percentage = 1,
        Value = 2,
    }

    public enum UnitMeasurement
    {
        Length = 1,
        Weight = 2,
        Number = 3,
        Kg = 4,
        Gram = 5,
        Meter = 6,
        Km = 7,
        Cm = 8,
        mm = 9,
        Inch = 10,
        Feet = 11,
        SqMeter = 12,
        Litre=13,
        Cubicmeter=14,
        Second=15,
        Hours=16,
        Minute=17,
        Days=18,

    }

    public enum QuotationType
    {
        Contractor = 1,
        Supplier = 2,
    }

    public enum PaymentType
    {
        Advance = 1,
        AfterDays = 2,
        Other = 3,
    }

    public enum TermAndConditionType
    {
        Contractor = 1,
        Supplier = 2,
        Both = 3,
    }

    public enum PaymentMode
    {
        Cash = 1,
        Cheque = 2,
        DD = 3,
        Ohers = 4,
    }

    public enum StockUpdateType
    {
        StockReceive = 1,
        StockIssue = 2,
    }

    public enum InvoiceType
    {
        Advance = 1,
        Normal = 2,
    }

    public enum TaxType
    {
        AllInclusive = 1,
        Exclusive = 2,
        NotApplicable = 3,
    }

    public enum PaymentStatus
    {
        PartPayment=1,
        FullPayment=2,
    }
}
