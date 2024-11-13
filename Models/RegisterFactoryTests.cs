using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeChallengeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeMVC.Models.Tests
{
    [TestClass()]
    public class RegisterFactoryTests
    {
        string expectedRegistersJsonString = @"
[
  {
    ""id"": 110,
    ""name"": ""Register 0110"",
    ""location"": ""East Nicolettetown"",
    ""status"": ""Online"",
    ""total"": 277,
    ""capacity"": 500,
    ""tenders"": [
      {
        ""denomination"": 20,
        ""count"": 7
      },
      {
        ""denomination"": 10,
        ""count"": 9
      },
      {
        ""denomination"": 5,
        ""count"": 4
      },
      {
        ""denomination"": 1,
        ""count"": 27
      }
    ]
  },
  {
    ""id"": 120,
    ""name"": ""Register 0120"",
    ""location"": ""East Magdalena"",
    ""status"": ""Online"",
    ""total"": 458,
    ""capacity"": 500,
    ""tenders"": [
      {
        ""denomination"": 20,
        ""count"": 10
      },
      {
        ""denomination"": 10,
        ""count"": 20
      },
      {
        ""denomination"": 5,
        ""count"": 4
      },
      {
        ""denomination"": 1,
        ""count"": 38
      }
    ]
  }
]";


        [TestMethod()]
        public void GetRegistersTest()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
            httpResponse.Content = new StringContent(expectedRegistersJsonString);

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(a => a.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(httpResponse);


            RegisterFactory RegisterFactory = new RegisterFactory(mockHttpClientWrapper.Object);
            List<Register> registers = RegisterFactory.GetRegisters();

            Assert.AreEqual(2, registers.Count());
        }

        [TestMethod()]
        public void SearchRegistersTest()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
            httpResponse.Content = new StringContent(expectedRegistersJsonString);

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(a => a.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(httpResponse);


            RegisterFactory RegisterFactory = new RegisterFactory(mockHttpClientWrapper.Object);
            List<Register> registers = RegisterFactory.SearchRegisters("East Magdalena");

            Assert.AreEqual(1, registers.Count());
        }
    }
}