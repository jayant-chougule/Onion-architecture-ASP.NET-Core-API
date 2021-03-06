﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;
using OnionArchitecture.Data;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistence.Contract;
using OnionArchitecture.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Test.Unit.Persistence
{
    public class CustomerRepositoryTest
    {
        private DbContextOptionsBuilder builder;

        private CustomerContext context;

        [SetUp]
        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("InMemoryCustomerDB");

            SettingUp();

        }

        [Test]
        public async Task CheckCustomerRepositoryGetAllCustomersAsyn()
        {
            var customerRepository = new CustomerRepository(context);
            var result = await customerRepository.GetAllCustomersAsync();
            Assert.LessOrEqual(2, result.Count());
        }

        [Test]
        public async Task CheckCustomerRepositoryGetCustomerAsync()
        {
            string name = "Shweta Naik";
            var customerRepository = new CustomerRepository(context);
            var result = await customerRepository.GetCustomerAsync(name);
            Assert.AreEqual(name, result.CustomerName);
        }

        void SettingUp()
        {
            // Inserting to inmemory database
            context = new CustomerContext(builder.Options);
            var customerRepository = new GenericRepository<Customer>(context);
            customerRepository.Add(new Customer { CustomerName = "Shweta Naik", Address = "Bangalore" });
            customerRepository.Add(new Customer { CustomerName = "Amit Naik", Address = "Bangalore" });
            customerRepository.SaveChanges();
        }
    }
}
