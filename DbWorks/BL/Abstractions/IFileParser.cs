using System;
using System.Collections.Generic;
using BL.SalesDataSourceDTOs;

namespace BL.Abstractions
{
    public interface IFileParser
    {
        IEnumerable<SalesDataSourceDTO> ReadFile();
    }
}