﻿using System;

namespace BL.Abstractions
{
    public interface IFileParser : IDisposable
    {
        ISalesDataSourceDTORaw ReadFile();
    }
}