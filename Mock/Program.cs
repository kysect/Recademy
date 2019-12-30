using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Mock.Generators;
using Recademy.Context;
using Recademy.Models;
using Recademy.Types;

namespace Mock
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using Mocker mock = new Mocker();
            mock.Mock();    
        }
    }
}