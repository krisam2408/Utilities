﻿namespace SpiegelHase.DataTransfer;

public sealed class ForwardParameter : BackParameter
{
    public string ForwardId { get; set; }

    public ForwardParameter(string forwardId, string controller, string action = "index", string backId = "") : base(controller, action, backId)
    {
        ForwardId = forwardId;
    }
}
