using System;

public class SolutionClass
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public int Amount { get; set; }
        public bool PaymentStatus { get; set; }
    }
}

