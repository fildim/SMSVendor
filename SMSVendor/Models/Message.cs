using System;
using System.Collections.Generic;

namespace SMSVendor.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Recipient { get; set; } = null!;

    public string Text { get; set; } = null!;

    public DateTime Created { get; set; }
}
